using API.DTOs;
using API.Entities;
using API.Extinsions;
using API.Interfaces;
using API.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class PhotosController : BaseGenericApiController<Photo,PhotoDto,PhotoDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileRepository _fileRepository;

        public PhotosController( IUnitOfWork uow) : base (uow)
        {
            _uow = uow;
            _fileRepository = _uow.FileRepository;
        }

        [HttpPost("AddPhoto")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            var url = await _fileRepository.CreateAsync(file, "photos");

            if (url == null) return BadRequest("No Photo Url");

            var photo = new Photo
            {
                Url = url,
                IsDeleted = false
            };

            if (user.Photos.Count == 0) photo.IsMain = true;

            user.Photos.Add(photo);

            if (await _uow.SaveAsync())
                return _uow.Mapper.Map<PhotoDto>(photo);


            return BadRequest("Problem adding photo");
        }
    }
}