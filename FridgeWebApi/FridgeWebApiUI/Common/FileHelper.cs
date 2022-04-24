using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FridgeWebApiBL.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FridgeWebApiUI.Common
{
    public class FileHelper : IFileHelper
    {
        public async Task TryUploadFile(IFormFile file, string fileName, string subFolder)
        {
            if (file is null)
                throw new ElementNullReferenceException("File is null");

            var s = file.ContentType;
            var code = "." + s.Substring(s.IndexOf("/", StringComparison.Ordinal) + "/".Length);

            var directory = Path.Combine(Directory.GetCurrentDirectory(), $"{subFolder}");
            var path = Path.Combine(directory, fileName + code);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (new FileInfo(path).Exists)
                throw new ElementAlreadyExistException("Picture already exist");

            try
            {
                await using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
            }
            catch
            {
                throw new ElementCannotLoadException("File cannot load");
            }
        }

        public async Task<FileContentResult> TryDownloadFile(string fileName, string subFolder)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new NullReferenceException("FileName is null or empty");

            var directory = Path.Combine(Directory.GetCurrentDirectory(), $"{subFolder}");
            var path = Path.Combine(directory, fileName);

            var isExistFile = Directory.GetFiles(directory).Contains(directory + '\\' + fileName);

            if (isExistFile)
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    var bytes = new byte[stream.Length];
                    await stream.ReadAsync(bytes);


                    return new FileContentResult(bytes, fileName);
                }
            }

            throw new ElementCannotLoadException("Cannot receive the file");
        }
    }
}
