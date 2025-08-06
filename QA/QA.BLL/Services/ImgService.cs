using Microsoft.JSInterop;
using QA.BLL.Interfaces;
using QA.Domain.Models;

namespace QA.BLL.Services
{
    public class ImgService : IImgService
    {
        private readonly IJSRuntime _jSRuntime;
        private readonly IUnitOfWork _unitOfWork;

        public ImgService(IUnitOfWork unitOfWork, IJSRuntime jSRuntime)
        {
            _unitOfWork = unitOfWork;
            _jSRuntime = jSRuntime;
        }

        public async Task DeleteFileFromDisk(Image img)
        {

            if (File.Exists(img.ImageUrl))
            {
                try
                {
                    File.Delete(img.ImageUrl);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task DeleteImage(Image img)
        {
            bool confirmed = false;
            confirmed = await _jSRuntime.InvokeAsync<bool>("confirm", $"Czy na pewno chcesz usuną obraz?");

            if (confirmed)
            {
                _unitOfWork.Image.Delete(img);
                await _unitOfWork.CompleteAsync();
                await DeleteFileFromDisk(img);
            }
        }
    }
}
