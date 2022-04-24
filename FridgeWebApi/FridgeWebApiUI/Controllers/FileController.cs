using FridgeWebApiBL.Exceptions;
using FridgeWebApiUI.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FridgeWebApiUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileHelper fileHelper;
        private readonly IConfiguration configuration;
        public FileController(IFileHelper fileHelper, IConfiguration configuration)
        {
            this.fileHelper = fileHelper;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<string>> UploadProductPicture(IFormFile file, string fileName)
        {
            if (file is null)
                throw new ElementNullReferenceException("File is null");

            if (file.ContentType != ImageContentType.ImageJpeg)
                throw new ElementDoesNotMatchException("Use file like jpeg");

            var subFolder = this.configuration.GetSection("Assets:Products").Value;

            await this.fileHelper.TryUploadFile(file, fileName, subFolder);

            return new JsonResult("Success");
        }

        [HttpGet]
        [Route("[action]/{fileName}")]
        public async Task<FileResult> DownloadProductPicture(string fileName = default)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new NullReferenceException("FileName is null or empty");

            var subFolder = this.configuration.GetSection("Assets:Products").Value;

            var directory = Path.Combine(Directory.GetCurrentDirectory(), $"{subFolder}");
            var path = Path.Combine(directory, fileName);

            var isExistFile = Directory.GetFiles(directory).Contains(directory + '\\' + fileName);

            if (!isExistFile)
                throw new ElementCannotLoadException("Cannot receive the file");

            var binary = await System.IO.File.ReadAllBytesAsync(path);
            return File(binary, ImageContentType.ImageJpeg);
        }
    }
}
