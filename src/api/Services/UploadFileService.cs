
using api.Helpers;
using api.Interfaces;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace api.Services
{
    public class UploadFileService : IUploadFileService
    {
        private readonly Cloudinary _cloudinary;
        private readonly long _maxFileSize = 5 * 1024 * 1024; // 5MB
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
        public UploadFileService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<UploadFileResult> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file uploaded.");

            if (file.Length > _maxFileSize)
                throw new ArgumentException($"File size exceeds {_maxFileSize / (1024 * 1024)}MB limit.");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension)
                || !_allowedExtensions.Contains(extension))
                throw new ArgumentException("Invalid file type.");

            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    PublicId = Guid.NewGuid().ToString(),
                    Overwrite = true,
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                return new UploadFileResult
                {
                    Url = uploadResult.Url.ToString(),
                    PublicId = uploadResult.PublicId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("File upload failed.", ex);
            }
        }

        public async Task<DeletionFileResult> DeleteFileAsync(string publicId)
        {
            if (string.IsNullOrWhiteSpace(publicId))
                throw new ArgumentException("Public ID cannot be empty.");

            try
            {
                var deleteParams = new DeletionParams(publicId);
                var result = await _cloudinary.DestroyAsync(deleteParams);

                return new DeletionFileResult
                {
                    Success = result.Result == "ok",
                    Message = result.Result == "ok" ? "File deleted successfully" : result.Error?.Message
                };
            }
            catch (Exception ex)
            {
                return new DeletionFileResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}