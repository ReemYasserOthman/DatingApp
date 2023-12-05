using Microsoft.AspNetCore.Http;

namespace API.Interfaces

{
    public interface IFileRepository
    
    {
        Task<string> CreateAsync(IFormFile file, string path);
        Task<string> CreateBase64Async(string base64String, string path);
        public void DeleteFile(string path);

    }
}