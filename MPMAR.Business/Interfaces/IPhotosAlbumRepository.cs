using MPMAR.Business.ViewModels;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IPhotosAlbumRepository
    {
        PhotosAlbumVersion Add(PhotosAlbumVersion photosAlbumVersion,int pageRouteId);
        PhotosAlbumVersion Update(PhotosAlbumVersion photosAlbumVersion, int pageRouteId);
        IEnumerable<PhotosAlbum> GetPhotosAlbumId(int albumMasterId);
        /// <summary>
        /// delete PhotosAlbum 
        /// </summary>
        /// <param name="id">PhotosAlbum id</param>
        /// <returns></returns>
        bool Delete(int id,int pageRouteId);
        IEnumerable<PhotosAlbumListViewModel> GetPhotoAlbums(int id);
        IEnumerable<PhotosAlbum> Get();
        PhotosAlbumEditViewModel GetDetail(int id);
        bool DeleteByPhotoArchiveId(List<PhotosAlbum> list);
    }
}
