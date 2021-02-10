using MPMAR.Business.ViewModels;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IPageSectionVersionRepository
    {  
        /// <summary>
        /// Add a new page section version to database
        /// </summary>
        /// <param name="pageSectionVersion">page section version model</param>
        /// <returns></returns>
        PageSectionVersion Add(PageSectionVersion pageSectionVersion);

        /// <summary>
        /// Update a page section object in database
        /// </summary>
        /// <param name="pageSectionVersion">page section version object</param>
        /// <returns></returns>
        PageSectionVersion Update(PageSectionVersion pageSectionVersion);

        /// <summary>
        /// Get all page section versions which contain page route version id sent in parameter
        /// </summary>
        /// <param name="pageRouteVerId">page route version id</param>
        /// <returns></returns>
        List<PageSectionListViewModel> GetPageSectionsByPageRouteId(int pageRouteVerId); 

        /// <summary>
        /// Get all page sections which contain page route id sent in parameter
        /// </summary>
        /// <param name="pageRouteId">page route id</param>
        /// <returns></returns>
        List<PageSection> GetPageSections(int pageRouteId);

        /// <summary>
        /// Delete a page section version from database by id
        /// </summary>
        /// <param name="id">page section version id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        PageSectionVersion Delete(int id, int pageRouteVersionId);

        /// <summary>
        /// Get page section version from database by id
        /// </summary>
        /// <param name="id">page section version id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        SectionViewModel Get(int id, int pageRouteVersionId);

        /// <summary>
        /// Get all page section versions which contain page route version id sent in parameter
        /// </summary>
        /// <param name="pageRouteVerId"></param>
        /// <returns></returns>
        List<PageSectionVersion> GetPageSectionVersions(int pageRouteVerId);

        /// <summary>
        /// Add new page route version to database
        /// </summary>
        /// <param name="pageRouteVersion">page route version model</param>
        /// <returns></returns>
        PageRouteVersion AddNewPageRouteVersion(PageRouteVersion pageRouteVersion);

        /// <summary>
        /// Coping page section and its cards to page section versions which has the same page route version id 
        /// </summary>
        /// <param name="pageRouteVersion">page route version object which i will take it's id to copy objects</param>
        /// <returns></returns>
        void CopyPageSectionVersions(PageRouteVersion pageRouteVersion);

        /// <summary>
        /// Update a page section object in database
        /// </summary>
        /// <param name="pageSectionVersion">page section version object</param>
        /// <returns></returns>
        void NormalUpdate(PageSectionVersion pageSectionVersion);
    }
}
