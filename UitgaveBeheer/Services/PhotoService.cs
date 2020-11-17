using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace UitgaveBeheer.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly string PhotoFolder = "PhotoFolder";
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;

        public PhotoService(IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
        }
        public string UploadPhoto(IFormFile photo)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            string pathName = Path.Combine(_hostEnvironment.WebRootPath, _configuration[PhotoFolder]);
            string fileNameWithPath = Path.Combine(pathName, uniqueFileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                photo.CopyTo(stream);
            }

            return $"/{_configuration[PhotoFolder]}/" + uniqueFileName;
        }

        public void DeletePhoto(string photoUrl)
        {
            string path = Path.Combine(_hostEnvironment.WebRootPath, photoUrl.Substring(1));
            System.IO.File.Delete(path);
        }
    }
}
