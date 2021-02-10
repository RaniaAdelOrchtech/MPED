using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MPMAR.Common.Helpers
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public FileService(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        /// <summary>
        /// upload image
        /// </summary>
        /// <param name="formFile">file</param>
        /// <returns>file name</returns>
        public string UploadImage(IFormFile formFile)
        {
            string uniqueFileName = null;
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            formFile.CopyTo(new FileStream(filePath, FileMode.Create));

            if (File.Exists(filePath))
            {
                return uniqueFileName;
                //uniqueFileName;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// upload new image url
        /// </summary>
        /// <param name="formFile">file</param>
        /// <returns>url</returns>
        public string UploadImageUrlNew(IFormFile formFile)
        {
            return UploadUrlWithFolder(formFile, "sharedImages");
        }

        /// <summary>
        /// upload new file with url
        /// </summary>
        /// <param name="formFile">File/param>
        /// <returns>url</returns>
        public string UploadFileUrlNew(IFormFile formFile)
        {
            return UploadUrlWithFolder(formFile, "sharedFiles");
        }

        /// <summary>
        /// upload image url
        /// </summary>
        /// <param name="formFile">file</param>
        /// <returns>file url</returns>
        public string UploadImageUrl(IFormFile formFile)
        {
            string uniqueFileName = null;
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            formFile.CopyTo(new FileStream(filePath, FileMode.Create));

            if (File.Exists(filePath))
            {
                return "http://212.129.13.21:800/Uploads/Images/" + uniqueFileName;
                //uniqueFileName;
            }
            else
            {
                return null;
            }
        }
        private string UploadUrlWithFolder(IFormFile formFile, string subFolderPath = "")
        {
            string uniqueFileName = null;
            var uploadsFolder = "";
            var mainFolderPath = _configuration.GetValue<string>("SharedImagesFolderURL");

            if (string.IsNullOrEmpty(mainFolderPath))
            {
                uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, subFolderPath);
            }
            else
            {
                uploadsFolder = Path.Combine(mainFolderPath, subFolderPath);
            }
            System.IO.Directory.CreateDirectory(uploadsFolder);

            uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName.Replace(" ", "_");
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            formFile.CopyTo(new FileStream(filePath, FileMode.Create));
            subFolderPath = subFolderPath != "" ? "/" + subFolderPath : "";

            if (File.Exists(filePath))
            {
                return subFolderPath + "/" + uniqueFileName;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// upload image with url
        /// </summary>
        /// <param name="formFile">file</param>
        /// <returns>image url</returns>
        public string UploadImageWithUrl(IFormFile formFile)
        {
            string uniqueFileName = null;
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            formFile.CopyTo(new FileStream(filePath, FileMode.Create));

            if (File.Exists(filePath))
            {
                //return "https://localhost:44387/Uploads/Images/" + uniqueFileName;
                return "http://212.129.13.21:800/Uploads/Images/" + uniqueFileName;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// upload file
        /// </summary>
        /// <param name="formFile">file</param>
        /// <returns>url</returns>
        public string UploadFile(IFormFile formFile)
        {
            string uniqueFileName = null;
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Files");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            formFile.CopyTo(new FileStream(filePath, FileMode.Create));

            if (File.Exists(filePath))
            {
                return uniqueFileName;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// remove image
        /// </summary>
        /// <param name="fileName">File name/param>
        /// <returns></returns>
        public void RemoveImage(string fileName)
        {
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images");
            string filePath = Path.Combine(uploadsFolder, fileName);
            if (File.Exists(filePath))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// remove image with url
        /// </summary>
        /// <param name="fileName">File name/param>
        /// <returns></returns>
        public void RemoveImageUrl(string fileName)
        {
            fileName = fileName.Replace("http://212.129.13.21:800/Uploads/Images/", "");
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images");
            string filePath = Path.Combine(uploadsFolder, fileName);
            if (File.Exists(filePath))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// remove file
        /// </summary>
        /// <param name="fileName">File name/param>
        /// <returns></returns>
        public void RemoveFile(string fileName)
        {
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Files");
            string filePath = Path.Combine(uploadsFolder, fileName);
            if (File.Exists(filePath))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// download file
        /// </summary>
        /// <param name="filename">File name/param>
        /// <returns>file</returns>
        public FileContentResult DownloadFile(string filename)
        {
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Files");
            string filePath = Path.Combine(uploadsFolder, filename);

            var mimeType = GetMimeType(filename);

            byte[] fileBytes = null;

            if (File.Exists(filePath))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                fileBytes = File.ReadAllBytes(filePath);
            }

            return new FileContentResult(fileBytes, mimeType)
            {
                FileDownloadName = filename
            };
        }

        private string GetMimeType(string fileName)
        {
            // Make Sure Microsoft.AspNetCore.StaticFiles Nuget Package is installed
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
