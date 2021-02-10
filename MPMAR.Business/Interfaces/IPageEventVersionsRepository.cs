using MPMAR.Business.ViewModels;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IPageEventVersionsRepository
    {
        /// <summary>
        /// add new PageEventVersions
        /// </summary>
        /// <param name="pageEventVer"></param>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        PageEventVersions Add(PageEventVersions pageEventVer, int pageRouteId);
        /// <summary>
        /// update PageEventVersions
        /// </summary>
        /// <param name="pageEventVer"></param>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        PageEventVersions Update(PageEventVersions pageEventVer, int pageRouteId);
        /// <summary>
        /// get PageEventVersions by page route id
        /// </summary>
        /// <param name="pageRouteId">page route id</param>
        /// <returns></returns>
        IEnumerable<PageEventViewModel> GetPageEventByPageRouteId(int pageRouteId);
        /// <summary>
        /// delete PageEventVersions by id
        /// </summary>
        /// <param name="id">PageEventVersions id</param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// get PageEventVersions by id
        /// </summary>
        /// <param name="id">PageEventVersions id</param>
        /// <returns></returns>
        PageEventVersionViewModel GetDetail(int id);
        /// <summary>
        /// get list of PageEventVersions
        /// </summary>
        /// <returns></returns>
        IEnumerable<PageEventVersions> GetAll();
        /// <summary>
        /// get list of PageEvent
        /// </summary>
        /// <returns></returns>
        IEnumerable<PageEvent> GetAllPageEvent();
        /// <summary>
        /// submit changes for the approval user 
        /// </summary>
        /// <param name="id">PageEventVersions id</param>
        /// <param name="userId"></param>
        /// <param name="pageLink"></param>
        /// <returns></returns>
        bool ApplySubmitRequest(int id, string userId, string pageLink);
        /// <summary>
        /// approve changes,change status to approved  and update the non version 
        /// </summary>
        /// <param name="id">PageEventVersions id</param>
        /// <param name="approvalId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Approve(int id, int approvalId, string userId);
        /// <summary>
        /// Ignore changes and change status to ignored
        /// </summary>
        /// <param name="id">PageEventVersions id</param>
        /// <param name="approvalId">approval notification id</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Ignore(int id, int approvalId, string userId);
    }
}
