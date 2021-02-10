using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IPageSectionRepository
    {
        /// <summary>
        /// Get all approved page section which contain same content id sent in parameter
        /// </summary>
        /// <param name="contentId">page section content id</param>
        /// <returns></returns>
        List<PageSection> GetApprovedPageSectionsByContentId(int contentId);
        
        /// <summary>
        /// Get all page section types
        /// </summary>
        /// <returns></returns>
        List<PageSectionType> GetPageSectionTypes();

        /// <summary>
        /// Get single page section type by id
        /// </summary>
        /// <param name="id">page section type id</param>
        /// <returns></returns>
        PageSectionType GetPageSectionType(int id);

        /// <summary>
        /// check if page section exist or not
        /// </summary>
        /// <param name="id">page section id</param>
        /// <returns></returns>
        bool ifPageSectionExist(int id);

        /// <summary>
        /// Delete page section by id
        /// </summary>
        /// <param name="id">page section id</param>
        /// <returns></returns>
        bool SoftDelete(int id);

        /// <summary>
        /// Get single page section by id
        /// </summary>
        /// <param name="id">page section id</param>
        /// <returns></returns>
        PageSection GetById(int id);

        /// <summary>
        /// Update page section from database
        /// </summary>
        /// <param name="pageSection">page section new data</param>
        /// <returns></returns>
        void Update(PageSection pageSection);

        /// <summary>
        /// Add a new page section object
        /// </summary>
        /// <param name="pageSection">page section model data</param>
        /// <returns></returns>
        void Add(PageSection pageSection);


    }
}
