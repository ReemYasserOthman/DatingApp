using API.Interfaces;

namespace API.Repositores

{
    public class FileRepository : IFileRepository
    {

        private readonly IHostEnvironment _hostEnvironment;
        public FileRepository(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task<string> CreateAsync(IFormFile file, string path)
        {
            try
            {
                if (file == null) return null;

                var fileName = file.FileName;

                //var extention = "." + fileName.Split('.')[fileName.Split('.').Length - 1];
                var extention = Path.GetExtension(fileName);

                var newFileName = Guid.NewGuid() + extention;

                //var pathDirectory = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/Upload", path);
                var pathDirectory = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "Upload", path);
                if (!Directory.Exists(pathDirectory))
                {
                    Directory.CreateDirectory(pathDirectory);
                }

                var pathFile = Path.Combine(pathDirectory, newFileName);


                using (var stream = System.IO.File.Create(pathFile))
                {
                    await file.CopyToAsync(stream);
                }


                return "Upload/" + path + "/" + newFileName;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        public async Task<string> CreateBase64Async(string base64String, string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(base64String)) return null;

                var bytes = Convert.FromBase64String(base64String);
                var pathDirectory = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "Upload", path);

                MemoryStream stream = new MemoryStream(bytes);

                var newFileName = Guid.NewGuid() + GetFileExtension(base64String);

                //IFormFile file = new FormFile(stream, 0, bytes.Length, newFileName, newFileName);


                if (!Directory.Exists(pathDirectory))
                {
                    Directory.CreateDirectory(pathDirectory);
                }

                var pathFile = Path.Combine(pathDirectory, newFileName);

                using (var st = File.Create(pathFile))
                {
                    await st.CopyToAsync(st);
                }

                return "Upload/" + path + "/" + newFileName;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            // await Task.Delay(1);
            // return "";

        }
        public static string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return ".png";
                case "/9J/4":
                    return ".jpg";
                case "AAAAF":
                    return ".mp4";
                case "JVBER":
                    return ".pdf";
                case "AAABA":
                    return ".ico";
                case "UMFYI":
                    return ".rar";
                case "E1XYD":
                    return ".rtf";
                case "U1PKC":
                    return ".txt";
                case "MQOWM":
                case "77U/M":
                    return ".srt";
                default:
                    return ".png";
            }
        }
        public void DeleteFile(string path)
        {
            try
            {
                File.Delete(Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/" + path));
            }
            catch (Exception)
            {

            }

        }

    }
}