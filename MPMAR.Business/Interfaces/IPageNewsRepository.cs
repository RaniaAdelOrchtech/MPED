using MPMAR.Business.ViewModels;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IPageNewsRepository
    {

        /// <summary>
        /// add new PageNewsVersion with new or updated page route 
        /// </summary>
        /// <param name="pageNews"></param>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        PageNewsVersion Add(PageNewsVersion pageNews, int pageRouteId);
        /// <summary>
        /// update PageNewsVersion with new or updated page route 
        /// </summary>
        /// <param name="PageNews"></param>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        PageNewsVersion Update(PageNewsVersion PageNews, int pageRouteId);
        /// <summary>
        /// get list of not deleted PageNewsType
        /// </summary>
        /// <returns></returns>
        List<PageNewsType> GetPageNewsTypes();
        /// <summary>
        /// get list of not deleted PageNewsType for specific ids
        /// </summary>
        /// <param name="id">list of PageNewsType ids</param>
        /// <returns></returns>
        List<PageNewsType> GetPageNewsType(List<int> id);
        /// <summary>
        /// get PageNewsListViewModel by pageRouteId
        /// </summary>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        IEnumerable<PageNewsListViewModel> GetPageNewsByPageRouteId(int pageRouteId);
        /// <summary>
        /// get list of page news ids that matched with the search word
        /// </summary>
        /// <param name="SearchWord"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        List<int> GetPageNewsBySearchWord(string SearchWord, string lang);
        /// <summary>
        /// get single page news by page news id
        /// </summary>
        /// <param name="PageNewsId"></param>
        /// <returns></returns>
        PageNews GetSinglePageNewsByPageNewsId(int PageNewsId);
        /// <summary>
        /// delet page news version by id
        /// </summary>
        /// <param name="id">page news version id</param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// submit to the approval user
        /// </summary>
        /// <param name="id">approval notification id</param>
        /// <param name="userId"></param>
        /// <param name="pageLink"></param>
        /// <returns></returns>
        bool ApplySubmitRequest(int id, string userId, string pageLink);
        /// <summary>
        /// get NewsViewModel by PageNewsVersions id
        /// </summary>
        /// <param name="id">PageNewsVersions id</param>
        /// <returns></returns>
        NewsViewModel Get(int id);
        /// <summary>
        /// get list of PageNews
        /// </summary>
        /// <returns></returns>
        IEnumerable<PageNews> GetPageNews();
        /// <summary>
        /// approve changes
        /// </summary>
        /// <param name="id">PageNewsVersions id</param>
        /// <param name="approvalId">ApprovalNotifications id</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Approve(int id, int approvalId, string userId);
        /// <summary>
        /// ignore changes 
        /// </summary>
        /// <param name="id">PageNewsVersions id</param>
        /// <param name="approvalId">ApprovalNotifications id</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Ignore(int id, int approvalId, string userId);

        IEnumerable<PageNews> GetPageNewsPaginate(int pageNum, int typeId, out int totalCount,string lang);
    }
}

