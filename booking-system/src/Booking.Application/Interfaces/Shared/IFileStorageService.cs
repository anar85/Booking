using Booking.Application.Models.DTOs.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces.Shared
{
    public interface IFileStorageService
    {
        Task<string> Upload(FileRequest request);
        Task<FileResponse> Download(string imagePath);
        Task Delete(string imagePath);
    }
}
