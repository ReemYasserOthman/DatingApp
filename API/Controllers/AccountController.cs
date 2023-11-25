using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers;

public class AccountController :BaiseApiController
{
  private readonly DataContext _context;
  private readonly ITokenService _tokenService;

    public AccountController(DataContext context, ITokenService tokenService)
    {
       _tokenService = tokenService;
       _context = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>>Register(RegisterDto registerDto)
    {

       if(await UserExists(registerDto.Username))
       return BadRequest("UserName is Taken!");

       using var hmac = new HMACSHA512();

       var user = new AppUser {
        UserName = registerDto.Username.ToLower(),
        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        PasswordSalt = hmac.Key
       };

       _context.Users.Add(user);
       await _context.SaveChangesAsync();

       return new UserDto{
        Username = user.UserName,
        Token = _tokenService.CreateToken(user)
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
        PhotoUrl = user.Photos.FirstOrDefault(p=> p.IsMain)?.Url        
        };

    } 

    private async Task<bool> UserExists(string userName)
    {
      return await _context.Users.AnyAsync(u=> u.UserName == userName.ToLower());
    }

}