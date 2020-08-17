using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SharedLibrary.Interfaces;

namespace ArtlistConverter.DAL
{
    public class UploadFilesDAL
    {
        public static async Task<IFile> Upload(IFormFile file)
        {
            try
            {
                var pathToSaveFiles = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    @"Artlist\Uploaded Files");

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Replace("\"", "").Trim();
                    var fullFilePath = Path.Combine(pathToSaveFiles, $@"{DateTime.Today.ToString("dd-MM-yyyy")}_{fileName}");
                    using (var fileStream = new FileStream(fullFilePath, FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fileStream);
                    }

                    var uploadedFile = new SharedLibrary.File(new FileExtension(Path.GetExtension(fullFilePath).Replace(".", "")))
                    {
                        Name = Path.GetFileNameWithoutExtension(fullFilePath),
                        Path = Path.GetDirectoryName(fullFilePath),
                    };

                    return uploadedFile;
                }
                else
                {
                    throw new ArgumentException("File length is not larger than 0.", "file");
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
