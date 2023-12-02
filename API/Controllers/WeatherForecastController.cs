using API.DTOs;
using API.Entities;
using API.Error;
using API.Interfaces;
using API.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : BaseGenericApiController<Photo, PhotoDto, PhotoDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IRepository<Photo> _Repo;
    public WeatherForecastController(IUnitOfWork uow) : base(uow)
    {
        _uow = uow;
        _Repo = _uow.Repository<Photo>();
    }

    [HttpPost("add")]
    public override async Task<IActionResult> Add(PhotoDto dto)
    {
        var x = _uow.Mapper.Map<Photo>(dto);

        // var result = _Repo.Add(x);

        // if (!await _uow.SaveAsync()) return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));

        // var map = await _Repo.Map_GetByAsync<PhotoDto>(x => x.Id == result.Id);

        // return Ok(map);

        return await base.Add(x);
    }

}
