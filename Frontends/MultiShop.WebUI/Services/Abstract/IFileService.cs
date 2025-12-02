namespace MultiShop.WebUI.Services.Abstract
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName);
    }
}
