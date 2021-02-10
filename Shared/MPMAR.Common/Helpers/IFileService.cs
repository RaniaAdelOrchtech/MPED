using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Common.Helpers
{
    public interface IFileService
    {
        /// <summary>
        /// upload image
        /// </summary>
        /// <param name="formFile">file</param>
        /// <returns></returns>
        string UploadImage(IFormFile formFile);

        /// <summary>
        /// upload image url
        /// </summary>
        /// <param name="formFile">file</param>
        /// <returns></returns>
        string UploadImageUrl(IFormFile formFile);

        /// <summary>
        /// upload new image url
        /// </summary>
        /// <param name="formFile">file</param>
        /// <returns></returns>
        string UploadImageUrlNew(IFormFile formFile);

        /// <summary>
        /// upload image with url
        /// </summary>
        /// <param name="formFile">file</param>
        /// <returns></returns>
        string UploadImageWithUrl(IFormFile formFile);

        /// <summary>
        /// upload file
        /// </summary>
        /// <param name="formFile">file</param>
        /// <returns></returns>
        string UploadFile(IFormFile formFile);

        /// <summary>
        /// remove image
        /// </summary>
        /// <param name="fileName">File name/param>
        /// <returns></returns>
        void RemoveImage(string fileName);

        /// <summary>
        /// remove file
        /// </summary>
        /// <param name="fileName">File name/param>
        /// <returns></returns>
        void RemoveFile(string fileName);

        /// <summary>
        /// remove image with url
        /// </summary>
        /// <param name="fileName">File name/param>
        /// <returns></returns>
        void RemoveImageUrl(string fileName);

        /// <summary>
        /// upload new file with url
        /// </summary>
        /// <param name="formFile">File/param>
        /// <returns></returns>
        string UploadFileUrlNew(IFormFile formFile);

        /// <summary>
        /// download file
        /// </summary>
        /// <param name="filename">File name/param>
        /// <returns></returns>
        public FileContentResult DownloadFile(string filename);
    }
}
