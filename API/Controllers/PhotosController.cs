using System.IO;
using API.DTOs;
using API.Entities;
using API.Extinsions;
using API.Interfaces;
using API.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

   
   public class PhotosController : BaseApiController
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileRepository _fileRepository;

        public PhotosController( IUnitOfWork uow) 
        {
            _uow = uow;
            _fileRepository = _uow.FileRepository;
        }
        
        [HttpPost("AddPhotoFile")]
        public async Task<ActionResult<PhotoDto>> AddPhotoFile(IFormFile file)
        {
            var user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            var url = await _fileRepository.CreateAsync(file, "users");
            //var fullPath = "http://localhost:5001/Upload/users" + url;
            
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
        
    
    
       
        [HttpPost("AddPhoto")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(string base64String)
        {
            var user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            var url = await _fileRepository.CreateBase64Async(base64String, "users");
            //var fullPath = "http://localhost:5001/api/wwwrot/" + url;
            
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

