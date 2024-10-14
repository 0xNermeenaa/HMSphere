using HMSphere.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace HMSphere.Application.Helpers
{
    public interface IUserHelpers
    {
        Task<ApplicationUser> GetCurrentUserAsync();
        //Task<string> AddFileAsync(IFormFile file, string folderName);
        //Task<bool> DeleteFileAsync(string imagePath, string folderName);
    }
}
