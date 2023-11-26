using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers;

public class AccountController :BaiseApiController
{
  private readonly DataContext _context;
  private readonly ITokenService _tokenService;

    private  readonly IMapper _mapper ;

    public AccountController(DataContext context, ITokenService tokenService,
     IMapper mapper)
    {
       _tokenService = tokenService;
        _mapper = mapper;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>>Register(RegisterDto registerDto)
    {

       if(await UserExists(registerDto.Username))
       return BadRequest("UserName is Taken!");
        
       var user = _mapper.Map<AppUser>(registerDto); 
       using var hmac = new HMACSHA512();

        user.UserName = registerDto.Username.ToLower();
        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
        user.PasswordSalt = hmac.Key;


       _context.Users.Add(user);
       await _context.SaveChangesAsync();

       return new UserDto{
        Username = user.UserName,
        Token = _tokenService.CreateToken(user),
        KnwonAs= user.KnownAs,
        Gender = user.Gender
       };

    }
    

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
      var user = await _context.Users
      .Include(p=> p.Photos)
      .SingleOrDefaultAsync(u =>u.UserName == loginDto.Username);
      
      if(user == null)
      return Unauthorized("Invalid UserName!");

      using var hmac = new HMACSHA512(user.PasswordSalt);
      var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

      for(int i = 0; i< computeHash.Length; i++){
         if(computeHash[i] != user.PasswordHash[i])
         return Unauthorized("Invalid Password!");
      }      
      
      return new UserDto{
        Username = user.UserName,
        Token = _tokenService.CreateToken(user),
        PhotoUrl = user.Photos.FirstOrDefault(p=> p.IsMain)?.Url,
        KnwonAs= user.KnownAs,
        Gender = user.Gender        
        };

    } 

    private async Task<bool> UserExists(string userName)
    {
      return await _context.Users.AnyAsync(u=> u.UserName == userName.ToLower());
    }

}