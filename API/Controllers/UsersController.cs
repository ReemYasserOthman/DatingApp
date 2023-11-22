using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Interfaces;

namespace API.Controllers;

[Authorize]
public class UsersController : BaiseApiController
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
       return Ok(await _userRepository.GetUsersAsync());
    }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<AppUser>> GetUserById(int id) =>
    // await _userRepository.GetUserByIdAsync(id);

   [HttpGet("{username}")]
    public async Task<ActionResult<AppUser>> GetUser(string username) => 
    await _userRepository.GetUserByUsernameAsync(username);

}
