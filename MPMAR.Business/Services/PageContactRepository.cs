using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.Business.Services
{
    public class PageContactRepository : IPageContactRepository
    {
        private readonly ApplicationDbContext _db;


        public PageContactRepository(ApplicationDbContext db)

        {
            _db = db;
        }

        public PageContact Add(PageContact pageContact)
        {
            try
            {
                pageContact.PageRouteVersionId = null;
                _db.PageContact.Add(pageContact);
                _db.SaveChanges();
                //return _db.PageContact.Include(x => x.PageRouteVersion).FirstOrDefault(c => c.Id == pageContact.Id);
                return _db.PageContact.FirstOrDefault(c => c.Id == pageContact.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PageContact Update(PageContact pageContact)
        {
            try
            {

                pageContact.PageRouteVersionId = null;
                _db.PageContact.Attach(pageContact);
                _db.Entry(pageContact).State = EntityState.Modified;
                _db.SaveChanges();
               
                return _db.PageContact.FirstOrDefault(c => c.Id == pageContact.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<PageContact> GetPageContactByPageId(int pageRouteVersionsId)
        {
            
            var pageContacts = _db.PageContact.Where(s => s.PageRouteVersionId == pageRouteVersionsId).OrderBy(s => s.Id).ToList();
            return pageContacts;
        }

        public PageContact Delete(int id)
        {
            try
            {
                var item = _db.PageContact.FirstOrDefault(x => x.Id == id);
                item.IsDeleted = true;

                _db.PageContact.Attach(item);
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                return _db.PageContact.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PageContact Get(int id)
        {

            return _db.PageContact.Include(x => x.PageRouteVersions).FirstOrDefault(p => p.PageRouteVersionId == id);
        }
        public PageContact GetDetail(int id)
        {
            //!(p.IsDeleted && p.PageRouteVersion.Status.Id == (int)RequestStatus.Approved) &&

            return _db.PageContact.Include(x => x.PageRouteVersions).FirstOrDefault(p => p.Id == id);
        }

        public PageContactVersions GetByPageContactId(int id)
        {
            return _db.PageContact.Where(i => i.Id == id).Select(
                pgMinisty => new PageContactVersions()
                {
                    Id = pgMinisty.Id,
                    IsActive = pgMinisty.IsActive,
                    IsDeleted = pgMinisty.IsDeleted,
                    ApprovalDate = pgMinisty.ApprovalDate,
                    ApprovedById = pgMinisty.ApprovedById,
                    CreatedById = pgMinisty.CreatedById,
                    CreationDate = pgMinisty.CreationDate,
                    ModificationDate = pgMinisty.ModificationDate,
                    ModifiedById = pgMinisty.ModifiedById,
                    ArAddress = pgMinisty.ArAddress,
                    ArMapTitle = pgMinisty.ArMapTitle,
                    PhoneNumber = pgMinisty.PhoneNumber,
                    Order = pgMinisty.Order,
                    MapUrl = pgMinisty.MapUrl,
                    FaxNumber = pgMinisty.FaxNumber,
                    ArPageName = pgMinisty.ArPageName,
                    ArParticipateTitle = pgMinisty.ArParticipateTitle,
                    EmailParticipateEmail = pgMinisty.EmailParticipateEmail,
                    EnAddress = pgMinisty.EnAddress,
                    EnMapTitle = pgMinisty.EnMapTitle,
                    EnPageName = pgMinisty.EnPageName,
                    EnParticipateTitle = pgMinisty.EnParticipateTitle,
                    FormParticipateActive = pgMinisty.FormParticipateActive,
                    SeoDescriptionAR = pgMinisty.SeoDescriptionAR,
                    SeoTwitterCardEN = pgMinisty.SeoTwitterCardEN,
                    SeoTwitterCardAR = pgMinisty.SeoTwitterCardAR,
                    SeoTitleEN = pgMinisty.SeoTitleEN,
                    SeoTitleAR = pgMinisty.SeoTitleAR,
                    SeoOgTitleEN = pgMinisty.SeoOgTitleEN,
                    SeoOgTitleAR = pgMinisty.SeoOgTitleAR,
                    SeoDescriptionEN = pgMinisty.SeoDescriptionEN,
                    PageRouteVersionId = pgMinisty.PageRouteVersionId
                }).FirstOrDefault();
        }
    }
}
