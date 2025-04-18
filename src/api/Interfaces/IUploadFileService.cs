using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Helpers;

namespace api.Interfaces
{
    public interface IUploadFileService
    {
        Task<UploadFileResult> UploadFileAsync(IFormFile file);
        Task<DeletionFileResult> DeleteFileAsync(string publicId);
    }


}