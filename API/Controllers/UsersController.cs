using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Interfaces;
using AutoMapper;
using API.DTOs;
using System.Security.Claims;

namespace API.Controllers;

[Authorize]
public class UsersController : BaiseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        return Ok(await _userRepository.GetMembersAsync());
        
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
       return await _userRepository.GetMemberAsync(username);
    } 

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _userRepository.GetUserByUsernameAsync(username);

        _mapper.Map(memberUpdateDto, user);

        _userRepository.Update(user);

        if (await _userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update user");
    }


    // [HttpGet("{id}")]
    // public async Task<ActionResult<AppUser>> GetUserById(int id) =>
    // await _userRepository.GetUserByIdAsync(id);

    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    // {
    //     var users = await _userRepository.GetUsersAsync();
    //     return Ok( _mapper.Map<IEnumerable<MemberDto>>(users) );
    // }

    //    [HttpGet("{username}")]
//     public async Task<ActionResult<MemberDto>> GetUser(string username)
//     {
//         var user = await _userRepository.GetUserByUsernameAsync(username);
//         return _mapper.Map<MemberDto>(user);
//     }


    


}
