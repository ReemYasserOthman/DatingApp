using System.Security.Cryptography;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers;

public class AccountController :BaiseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    private  readonly IMapper _mapper ;

    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService,
     IMapper mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _mapper = mapper;
       
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>>Register(RegisterDto registerDto)
    {

       if(await UserExists(registerDto.Username))
       return BadRequest("UserName is Taken!");
        
       var user = _mapper.Map<AppUser>(registerDto); 
       using var hmac = new HMACSHA512();

        user.UserName = registerDto.Username.ToLower();
        
       var result = await _userManager.CreateAsync(user,registerDto.Password);

       if(!result.Succeeded) return BadRequest(result.Errors);

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
      var user = await _userManager.Users
      .Include(p=> p.Photos)
      .SingleOrDefaultAsync(u =>u.UserName == loginDto.Username);
      
      if(user == null)
      return Unauthorized("Invalid UserName!");
      var result = await _userManager.CheckPasswordAsync(user, loginDto.Password); 
      if(!result) return Unauthorized("Invalied Password!");
      
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
      return await _userManager.Users.AnyAsync(u=> u.UserName == userName.ToLower());
    }

}