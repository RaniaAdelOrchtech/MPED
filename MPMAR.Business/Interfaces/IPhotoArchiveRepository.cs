using MPMAR.Business.ViewModels;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IPhotoArchiveRepository 
    {
        PhotoArchiveVersion Add(PhotoArchiveVersion albumMaster,int pageRouteId);
        PhotoArchiveVersion Update(PhotoArchiveVersion PhotoArchiveItem, int pageRouteId);
        IEnumerable<PhotoArchive> GetPhotoArchiveId(int albumMasterId);
        /// <summary>
        /// delete PhotoArchive
        /// </summary>
        /// <param name="id">PhotoArchive id</param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// get PhotoArchive by id
        /// </summary>
        /// <param name="id">PhotoArchive id</param>
        /// <returns></returns>
        PhotoArchive Get(int id);
        /// <summary>
        /// get list of PhotoArchive
        /// </summary>
        /// <returns></returns>
        IEnumerable<PhotoArchive> Get();
        PhotoArchiveEditViewModel GetDetail(int id);
        IEnumerable<PhotoArchiveListViewModel> GetPhotoArchiveByPageRouteId(int pageRouteId);
        PhotoArchiveVersion GetVersion(int id, bool isDeleted = false);
        PhotoArchiveVersion AddOrUpdatePhotoArchiveVersion(int pageRouteId, int photoArchiveVersionId);
        bool ApplySubmitRequest(int id, string userId, string pageLink);
        /// <summary>
        /// approve changes
        /// </summary>
        /// <param name="id">PhotoArchiveVersion id</param>
        /// <param name="approvalId">ApprovalNotifications id</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Approve(int id, int approvalId, string userId);
        /// <summary>
        /// ignore changes 
        /// </summary>
        /// <param name="id">PhotoArchiveVersion id</param>
        /// <param name="approvalId">ApprovalNotifications id</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Ignore(int id, int approvalId, string userId);
        IEnumerable<PhotoArchiveVersion> GetVersion();
        bool DeleteVer(int id, string userId, string pageLink);
    }
}
