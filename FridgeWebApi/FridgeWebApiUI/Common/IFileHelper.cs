using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FridgeWebApiUI.Common
{
    public interface IFileHelper
    {
        Task TryUploadFile(IFormFile file, string fileName, string subFolder);
        Task<FileContentResult> TryDownloadFile(string fileName, string subFolder);
    }
}