using Booking.Application.Exceptions;
using Booking.Application.Interfaces.Shared;
using Booking.Application.Models.Configs;
using Booking.Application.Models.Constants;
using Booking.Application.Models.DTOs.Files;
using Microsoft.Extensions.Options;

namespace Booking.Application.Shared.FileHelper
{
    public class FileStorageService : IFileStorageService
    {
        FileSetting _options;
        public FileStorageService(IOptions<FileSetting> options)
        {
            _options = options.Value;
        }
        public async Task<string> Upload(FileRequest request)
        {
            return await SaveFile(request);
        }

        public async Task<FileResponse> Download(string imagePath)
        {
            string filePath = $"{_options.StoragePath}{imagePath}";
            string content = "";
            string extention = "";

            if (File.Exists(filePath))//                throw new NotFoundException(ExceptionCodes.FileError, "File not found!");
            { 
             var bytes = await File.ReadAllBytesAsync(filePath);
                content = Convert.ToBase64String(bytes);
                extention = Path.GetExtension(imagePath);
            }
           
            var response = new FileResponse
            {
                Content = content,
                Extention =extention
            };
            return response;
        }

        public async Task Delete(string imagePath)
        {
            if (!String.IsNullOrEmpty(imagePath))
            {
                string file = $"{_options.StoragePath}{imagePath}";

                if (File.Exists(file))
                    File.Delete(file);
            }
        }

        #region privatesMetods

        private async Task<string> SaveFile(FileRequest request)
        {
            string filePath = string.Empty;

            if (request.File == null) return filePath;

            var extention = Path.GetExtension(request.File.FileName);
            Guid guid = Guid.NewGuid();
            string fileName = guid.ToString().Replace("-", "");

            var fileDirection = $@"{_options.StoragePath}{request.FilePath}";

            if (!Directory.Exists(fileDirection)) Directory.CreateDirectory(fileDirection);

            filePath = $@"{fileDirection}\{fileName}{extention}";

            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(fileStream);
            }

            return filePath.Replace(_options.StoragePath, "");
        }




        #endregion

    }
}
