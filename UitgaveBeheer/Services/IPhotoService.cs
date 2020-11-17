using Microsoft.AspNetCore.Http;

namespace UitgaveBeheer.Services
{
    public interface IPhotoService
    {
        string UploadPhoto(IFormFile photo);
        void DeletePhoto(string photoUrl);
    }
}
