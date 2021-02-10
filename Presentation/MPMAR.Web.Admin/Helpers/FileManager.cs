using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Helpers
{
    public class FileManager
    {
        public FileManager()
        {

        }

        public string UpluadImageToServer(IFormFile formFile, IWebHostEnvironment hostingEnvironment)
        {
            string uniqueFileName = null;
            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Uploads/Images");
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

        public string UpluadFileToServer(IFormFile formFile, IWebHostEnvironment hostingEnvironment)
        {
            string uniqueFileName = null;
            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Uploads/Files");
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

        public void RemoveImageFromServer(string fileName, IWebHostEnvironment hostingEnvironment)
        {
            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Uploads/Images");
            string filePath = Path.Combine(uploadsFolder, fileName);
            if (File.Exists(filePath))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(filePath);
            }
        }

        public void RemoveFileFromServer(string fileName, IWebHostEnvironment hostingEnvironment)
        {
            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Uploads/Files");
            string filePath = Path.Combine(uploadsFolder, fileName);
            if (File.Exists(filePath))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(filePath);
            }
        }
    }
}
