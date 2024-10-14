using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using CMS_back.Models;
using CMS_back.Consts;

namespace CMS_back.Application.Helpers
{
    public class UserHelpers : IUserHelpers
    {
        #region fields
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        #endregion

        #region ctor
        public UserHelpers(IConfiguration config, UserManager<ApplicationUser> userManager
            , IHttpContextAccessor contextAccessor
            , IWebHostEnvironment webHostEnvironment)
        {
            _config = config;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

   
        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var currentUser = _contextAccessor.HttpContext.User;
            return await _userManager.GetUserAsync(currentUser);
        }


        #region file handling
        public async Task<string> AddFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
            {
                return string.Empty;
            }

            string rootPath = _webHostEnvironment.WebRootPath;
            var user = await GetCurrentUserAsync();
            string userName = user.UserName;
            string profileFolderPath = "";
            if (folderName == ConstsRoles.AdminFaculity)
                profileFolderPath = Path.Combine(rootPath, "AdminFaculity", userName);
            else
                profileFolderPath = Path.Combine(rootPath, "Images", userName, folderName);
            if (!Directory.Exists(profileFolderPath))
            {
                Directory.CreateDirectory(profileFolderPath);
            }

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(profileFolderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            if (folderName == ConstsRoles.AdminUniversity)
                return $"/AdminUniversity/{userName}/{fileName}";
            return $"/Images/{userName}/{folderName}/{fileName}";

        }

        public async Task<bool> DeleteFileAsync(string filePath, string folderName)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return true;
            }

            string rootPath = _webHostEnvironment.WebRootPath;
            var user = await GetCurrentUserAsync();
            string userName = user.UserName;

            if (folderName == ConstsRoles.Staff)
            {
                if (!filePath.StartsWith($"/Staff/{userName}/"))
                {
                    throw new ArgumentException("Invalid file path.", nameof(filePath));
                }
            }

            else
            {
                if (!filePath.StartsWith($"/Images/{userName}/{folderName}/"))
                {
                    throw new ArgumentException("Invalid file path.", nameof(filePath));
                }
            }
            string fullFilePath = Path.Combine(rootPath, filePath.TrimStart('/'));

            if (File.Exists(fullFilePath))
            {
                File.Delete(fullFilePath);
                return true;
            }
            else
            {
                throw new FileNotFoundException("File not found.", fullFilePath);
            }

        }
        #endregion

    }
}