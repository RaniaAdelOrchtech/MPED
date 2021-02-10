using MPMAR.Business.Models;
using MPMAR.Business.ViewModels;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface ISectionCardVersionRepository

    { 
        /// <summary>
        /// Add a new page section card version to database
        /// </summary>
        /// <param name="sectionCardVersion">page section card object</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        PageSectionCardVersion Add(PageSectionCardVersion sectionCardVersion, int pageRouteVersionId);

        /// <summary>
        /// Update a page section card version from database
        /// </summary>
        /// <param name="sectionCardVersion">page section card object</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        PageSectionCardVersion Update(PageSectionCardVersion sectionCardVersion, int pageRouteVersionId);

        /// <summary>
        /// Get all section cards which contain the same section version id sent in paramater
        /// </summary>
        /// <param name="sectionVersionId">page section version id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        List<SectionCardListViewModel> GetCardsBySectionId(int sectionVersionId, int pageRouteVersionId);

        /// <summary>
        /// Get all section cards by section id
        /// </summary>
        /// <param name="sectionId">page section id</param>
        /// <returns></returns>
        IEnumerable<PageSectionCard> GetSectionCards(int sectionId);

        /// <summary>
        /// Delete a page section card version object by id
        /// </summary>
        /// <param name="id">page section card version id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        DeleteCardsViewModel Delete(int id, int pageRouteVersionId);

        /// <summary>
        /// Get page section card version object 
        /// </summary>
        /// <param name="id">page section card version id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        SectionCardEditViewModel Get(int id, int pageRouteVersionId);
    }
}
