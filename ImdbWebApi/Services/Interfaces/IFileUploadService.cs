using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ImdbWebApi.Services.Interfaces
{
    public interface IFileUploadService
    {
        Task<string> UploadFile(IFormFile file);
    }
}
