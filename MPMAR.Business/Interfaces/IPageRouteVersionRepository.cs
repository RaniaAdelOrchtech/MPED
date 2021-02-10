using Microsoft.AspNetCore.Http;
using MPMAR.Business.ViewModels;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.Business.Interfaces
{
    public interface IPageRouteVersionRepository
    {
        /// <summary>
        /// Adding a new page route version
        /// </summary>
        /// <param name="pageRouteVersion">page route version model</param>
        /// <returns></returns>
        PageRouteVersion Add(PageRouteVersion pageRouteVersion);

        /// <summary>
        /// Adding a new static page 
        /// </summary>
        /// <param name="PageRouteVersion">page route version model</param>
        /// <returns></returns>
        void AddStaticPage(PageRouteVersion PageRouteVersion);

        /// <summary>
        /// upgate a page route version model
        /// </summary>
        /// <param name="pageRouteVersion">page route version model</param>
        /// <returns></returns>
        PageRouteVersion Update(PageRouteVersion pageRouteVersion);

        /// <summary>
        /// get a single page route version
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <returns></returns>
        PageRouteVersion GetById(int id);

        /// <summary>
        /// get a single page route version
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <returns></returns>
        PageRouteVersion Get(int id);

        /// <summary>
        /// get page route versions objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<PageRouteVersion> Get();


        /// <summary>
        /// get all dynamic pages objects
        /// </summary>
        /// <returns></returns>
        List<PageRouteListViewModel> GetDynamicPages();

        /// <summary>
        /// get all static pages objects
        /// </summary>
        /// <returns></returns>
        List<PageRouteListViewModel> GetStaticPages();

        /// <summary>
        /// delete single page route version object
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <returns></returns>
        PageRouteVersion SoftDelete(int id);

        /// <summary>
        /// delete single page route version object
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// get single page route version by page route id
        /// </summary>
        /// <param name="id">page route id</param>
        /// <returns></returns>
        PageRouteVersion GetByPageRoute(int id);

        /// <summary>
        /// apply editing request method
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <param name="pageFilePathAr">arabic page file path</param>
        /// <param name="pageFilePathEn">english page file path</param>
        /// <returns></returns>
        PageRouteVersion ApplyEditRequest(int id, string pageFilePathAr, string pageFilePathEn);

        /// <summary>
        /// changing page route version status
        /// </summary>
        /// <param name="pageVersionId">page route version id</param>
        /// <param name="requestStatus">changed status</param>
        /// <returns></returns>
        void ChangeStatus(int pageVersionId, RequestStatus requestStatus);

        /// <summary>
        /// Applying submit request
        /// </summary>
        /// <param name="pageRouteVersion">page route version model</param>
        /// <param name="userId">logged in user</param>
        /// <param name="urlParent">parent route version url</param>
        /// <param name="urlSection">section url</param>
        /// <returns></returns>
        bool ApplySubmitRequest(PageRouteVersion pageRouteVersion, string userId, string urlParent, string urlSection);

        /// <summary>
        /// get page route version by page type
        /// </summary>
        /// <param name="pageType">page type</param>
        /// <returns></returns>
        PageRouteVersion GetPageRouteVersionByPageType(string pageType);

        /// <summary>
        /// Approve page route version
        /// </summary>
        /// <param name="pageRouteVersion">page route version model</param>
        /// <param name="changeType">change type</param>
        /// <returns></returns>
        void ApprovePageRoute(PageRouteVersion pageRouteVersion, ChangeType changeType);

        /// <summary>
        /// get page route version without model includes
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <returns></returns>
        PageRouteVersion GetByIdWithoutIncludes(int id);
        //PageRouteVersion GetLatestByRoutId(int pageRouteId);

        /// <summary>
        /// Adding or updating page route version
        /// </summary>
        /// <param name="pageRouteId">page route id</param>
        /// <returns></returns>
        PageRouteVersion AddOrUpdatePageRouteVersion(int pageRouteId);
        /// <summary>
        /// get media pages names
        /// </summary>
        /// <returns></returns>
        List<string> GetMediaPagesNames();

        PageRouteVersion GetWithNoTracking(int id);
        PageRouteVersion GetCurrentPageRouteVersionByPageRouteId(int? pageRouteId);
    }
}
