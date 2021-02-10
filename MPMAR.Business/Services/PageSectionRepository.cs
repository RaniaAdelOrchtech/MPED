using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class PageSectionRepository : IPageSectionRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IPageSectionVersionRepository _pageSectionVersionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string userName;

        public ILogger<PageSectionVersionRepository> _logger { get; }

        public PageSectionRepository(ApplicationDbContext db, IPageSectionVersionRepository pageSectionVersionRepository, ILogger<PageSectionVersionRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _pageSectionVersionRepository = pageSectionVersionRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            userName = _httpContextAccessor.HttpContext.User.Identity.Name;
        }

        /// <summary>
        /// Get all approved page section which contain same content id sent in parameter
        /// </summary>
        /// <param name="contentId">page section content id</param>
        /// <returns>List of page sections</returns>
        public List<PageSection> GetApprovedPageSectionsByContentId(int contentId)
        {
            return _db.PageSections.Include(s => s.PageSectionType).Where(s => !s.IsDeleted).ToList();
        }

        /// <summary>
        /// Get all page section types
        /// </summary>
        /// <returns>List of page section types</returns>
        public List<PageSectionType> GetPageSectionTypes()
        {
            return _db.PageSectionTypes.ToList();
        }

        /// <summary>
        /// Get single page section type by id
        /// </summary>
        /// <param name="id">page section type id</param>
        /// <returns>single page section type</returns>
        public PageSectionType GetPageSectionType(int id)
        {
            return _db.PageSectionTypes.FirstOrDefault(st => st.Id == id);
        }

        /// <summary>
        /// check if page section exist or not
        /// </summary>
        /// <param name="id">page section id</param>
        /// <returns>true if exist false otherwise</returns>
        public bool ifPageSectionExist(int id)
        {
            var ps = _db.PageSections.Find(id);
            if (ps == null)
                return false;
            return true;
        }

        /// <summary>
        /// Add a new page section object
        /// </summary>
        /// <param name="pageSection">page section model data</param>
        /// <returns></returns>
        public void Add(PageSection pageSection)
        {
            _db.PageSections.Add(pageSection);
            _db.SaveChanges();
        }

        /// <summary>
        /// Get single page section by id
        /// </summary>
        /// <param name="id">page section id</param>
        /// <returns>single page section</returns>
        public PageSection GetById(int id)
        {
            return _db.PageSections.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Update page section from database
        /// </summary>
        /// <param name="pageSection">page section new data</param>
        /// <returns></returns>
        public void Update(PageSection pageSection)
        {
            _db.PageSections.Attach(pageSection);
            _db.Entry(pageSection).State = EntityState.Modified;
            _db.SaveChanges();
        }

        /// <summary>
        /// Delete page section by id
        /// </summary>
        /// <param name="id">page section id</param>
        /// <returns>true if deleted false otherwise</returns>
        public bool SoftDelete(int id)
        {
            try
            {
                var model = GetById(id);
                if (model != null)
                {
                    model.IsDeleted = true;

                    Update(model);

                    var cards = _db.PageSectionCards.Where(d => d.PageSectionId == id).ToList();
                    foreach (var card in cards)
                    {
                        card.IsDeleted = true;
                        _db.PageSectionCards.Attach(card);
                        _db.Entry(card).State = EntityState.Modified;
                    }

                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }


        }

       
    }
}
