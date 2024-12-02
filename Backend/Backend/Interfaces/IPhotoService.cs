using CloudinaryDotNet.Actions;

namespace Backend.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo);

        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
