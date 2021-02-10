using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Business.ViewModels;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.Business.Services
{
    public class PageEventVersionsRepository : IPageEventVersionsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IPageRouteVersionRepository _pageRouteVersionRepository;

        public PageEventVersionsRepository(ApplicationDbContext db, IPageRouteVersionRepository pageRouteVersionRepository)
        {
            _db = db;
            _pageRouteVersionRepository = pageRouteVersionRepository;
        }

        public PageEventVersions Add(PageEventVersions pageEventVer, int pageRouteId)
        {
            try
            {
                var pageRouteVersion = _pageRouteVersionRepository.AddOrUpdatePageRouteVersion(pageRouteId);
                pageEventVer.PageRouteVersionId = pageRouteVersion.Id;
                pageEventVer.VersionStatusEnum = VersionStatusEnum.Draft;
                pageEventVer.ChangeActionEnum = ChangeActionEnum.New;
                _db.PageEventVersions.Add(pageEventVer);
                _db.SaveChanges();
                return _db.PageEventVersions.FirstOrDefault(c => c.Id == pageEventVer.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PageEventVersions Update(PageEventVersions pageEventVer, int pageRouteId)
        {
            try
            {
                var pageRouteVersion = _pageRouteVersionRepository.AddOrUpdatePageRouteVersion(pageRouteId);
                var existingEventVer = _db.PageEventVersions.Find(pageEventVer.Id);
                if (existingEventVer.VersionStatusEnum == VersionStatusEnum.Approved || existingEventVer.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    pageEventVer.Id = 0;
                    pageEventVer.VersionStatusEnum = VersionStatusEnum.Draft;
                    pageEventVer.ChangeActionEnum = ChangeActionEnum.Update;
                    pageEventVer.PageRouteVersionId = pageRouteVersion.Id;
                    _db.PageEventVersions.Add(pageEventVer);
                }
                else
                {
                    pageEventVer.VersionStatusEnum = VersionStatusEnum.Draft;
                    pageEventVer.PageRouteVersionId = pageRouteVersion.Id;
                    pageEventVer.ChangeActionEnum = existingEventVer.ChangeActionEnum == ChangeActionEnum.New ? ChangeActionEnum.New : ChangeActionEnum.Update;
                    _db.Entry(existingEventVer).CurrentValues.SetValues(pageEventVer);
                }
                _db.SaveChanges();
                return _db.PageEventVersions.FirstOrDefault(s => s.Id == pageEventVer.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<PageEventViewModel> GetPageEventByPageRouteId(int pageRouteId)
        {
            var pageRouteVersionId = 0;

            var pageRouteVersion = _pageRouteVersionRepository.GetByPageRoute(pageRouteId);

            if (pageRouteVersion != null) pageRouteVersionId = pageRouteVersion.Id;

            //join between version and non version PageEvents take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from nw in _db.PageEvents.Where(d => !d.IsDeleted && d.PageRouteId == pageRouteId)
                              from nwv in _db.PageEventVersions.Where(d => d.PageEventId == nw.Id && !d.IsDeleted
                              && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).Take(1)
                              select new PageEventViewModel
                              {
                                  Id = nw.Id,
                                  VerId = nwv.Id,
                                  EnTitle = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.EnTitle : nw.EnTitle,
                                  EnDescription = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.EnDescription : nw.EnDescription,
                                  ArTitle = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.ArTitle : nw.EnTitle,
                                  ArDescription = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.ArDescription : nw.ArDescription,
                                  IsActive = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.IsActive : nw.IsActive,
                                  VersionStatusEnum = nwv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  EventStartDate = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.EventStartDate : nw.EventStartDate,
                                  EventEndDate = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.EventEndDate : nw.EventEndDate,
                                  Order = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.Order : nw.Order,
                                  EnAddress = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.EnAddress : nw.EnAddress,
                                  ArAddress = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.ArAddress : nw.ArAddress
                              });

            //get the rest from PageEventVersions that wasn't included in previous join 
            var queryleft = (from nwv in _db.PageEventVersions.Where(d => !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored
                             && d.PageRouteVersionId == pageRouteVersionId)
                             where !_db.PageEvents.Any(d => d.Id == nwv.PageEventId)
                             select new PageEventViewModel
                             {
                                 Id = 0,
                                 VerId = nwv.Id,
                                 EnTitle = nwv.EnTitle,
                                 EnDescription = nwv.EnDescription,
                                 ArTitle = nwv.ArTitle,
                                 ArDescription = nwv.ArDescription,
                                 IsActive = nwv.IsActive,
                                 VersionStatusEnum = nwv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                 Order = nwv.Order,
                                 EventStartDate = nwv.EventStartDate,
                                 EventEndDate = nwv.EventEndDate,
                                 EnAddress = nwv.EnAddress,
                                 ArAddress = nwv.ArAddress
                             });
            return queryright.Union(queryleft).OrderByDescending(d => d.VerId).ToList();
        }

        public bool Delete(int id)
        {
            try
            {
                var existingEventVer = _db.PageEventVersions.Find(id);
                if (existingEventVer.VersionStatusEnum == VersionStatusEnum.Draft && existingEventVer.PageEventId == null)
                {
                    existingEventVer.IsDeleted = true;
                    existingEventVer.ChangeActionEnum = ChangeActionEnum.Delete;
                    _db.PageEventVersions.Update(existingEventVer);
                    _db.SaveChanges();
                    return true;
                }
                else
                {
                    var pageRouteVer = _db.PageRouteVersions.Find(existingEventVer.PageRouteVersionId);
                    var pageRouteVersion = _pageRouteVersionRepository.AddOrUpdatePageRouteVersion(pageRouteVer.PageRouteId.Value);

                    if (existingEventVer.VersionStatusEnum == VersionStatusEnum.Approved || existingEventVer.VersionStatusEnum == VersionStatusEnum.Ignored)
                    {
                        existingEventVer.Id = 0;
                        existingEventVer.VersionStatusEnum = VersionStatusEnum.Draft;
                        //existingEventVer.IsDeleted = true;
                        existingEventVer.ChangeActionEnum = ChangeActionEnum.Delete;
                        existingEventVer.PageRouteVersionId = pageRouteVersion.Id;
                        _db.PageEventVersions.Add(existingEventVer);
                    }
                    else
                    {
                        existingEventVer.VersionStatusEnum = VersionStatusEnum.Draft;
                        //existingEventVer.IsDeleted = true;
                        existingEventVer.ChangeActionEnum = ChangeActionEnum.Delete;
                        existingEventVer.PageRouteVersionId = pageRouteVersion.Id;
                        _db.PageEventVersions.Update(existingEventVer);
                    }
                    _db.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public PageEventVersionViewModel GetDetail(int id)
        {
            var eventVer = _db.PageEventVersions.FirstOrDefault(p => p.Id == id);
            if (eventVer != null)
            {
                if (eventVer.VersionStatusEnum == VersionStatusEnum.Approved || eventVer.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    var eventModel = _db.PageEvents.FirstOrDefault(p => p.Id == eventVer.PageEventId);
                    PageEventVersionViewModel eventViewModel = new PageEventVersionViewModel
                    {
                        Id = eventVer.Id,
                        EnTitle = eventModel.EnTitle,
                        ArTitle = eventModel.ArTitle,
                        EnDescription = eventModel.EnDescription,
                        ArDescription = eventModel.ArDescription,
                        IsActive = eventModel.IsActive,
                        PageEventId = eventModel.Id,
                        ArAddress = eventModel.ArAddress,
                        EventEndDate = eventModel.EventEndDate,
                        ArImageAlt = eventModel.ArImageAlt,
                        EnAddress = eventModel.EnAddress,
                        EnImageAlt = eventModel.EnImageAlt,
                        EventCaption = eventModel.EventCaption,
                        EventDateColor = eventModel.EventDateColor,
                        EventLocation = eventModel.EventLocation,
                        EventLocationUrl = eventModel.EventLocationUrl,
                        EventSocialLinks = eventModel.EventSocialLinks,
                        EventStartDate = eventModel.EventStartDate,
                        Order = eventModel.Order,
                        //ShowInHome= eventModel.ShowInHome,
                        SeoDescriptionAR = eventModel.SeoDescriptionAR,
                        SeoDescriptionEN = eventModel.SeoDescriptionEN,
                        SeoOgTitleAR = eventModel.SeoOgTitleAR,
                        SeoOgTitleEN = eventModel.SeoOgTitleEN,
                        SeoTitleAR = eventModel.SeoTitleAR,
                        SeoTitleEN = eventModel.SeoTitleEN,
                        SeoTwitterCardAR = eventModel.SeoTwitterCardAR,
                        SeoTwitterCardEN = eventModel.SeoTwitterCardEN,
                        Url = eventModel.EnUrl,
                        DetailUrl = eventModel.ArUrl
                    };

                    return eventViewModel;
                }
                else
                {
                    PageEventVersionViewModel eventViewModel = new PageEventVersionViewModel
                    {
                        Id = eventVer.Id,
                        EnTitle = eventVer.EnTitle,
                        ArTitle = eventVer.ArTitle,
                        EnDescription = eventVer.EnDescription,
                        ArDescription = eventVer.ArDescription,
                        IsActive = eventVer.IsActive,
                        PageEventId = eventVer.PageEventId,
                        ArAddress = eventVer.ArAddress,
                        EventEndDate = eventVer.EventEndDate,
                        ArImageAlt = eventVer.ArImageAlt,
                        EnAddress = eventVer.EnAddress,
                        EnImageAlt = eventVer.EnImageAlt,
                        EventCaption = eventVer.EventCaption,
                        EventDateColor = eventVer.EventDateColor,
                        EventLocation = eventVer.EventLocation,
                        EventLocationUrl = eventVer.EventLocationUrl,
                        EventSocialLinks = eventVer.EventSocialLinks,
                        EventStartDate = eventVer.EventStartDate,
                        Order = eventVer.Order,
                        //ShowInHome = eventVer.ShowInHome
                        SeoDescriptionAR = eventVer.SeoDescriptionAR,
                        SeoDescriptionEN = eventVer.SeoDescriptionEN,
                        SeoOgTitleAR = eventVer.SeoOgTitleAR,
                        SeoOgTitleEN = eventVer.SeoOgTitleEN,
                        SeoTitleAR = eventVer.SeoTitleAR,
                        SeoTitleEN = eventVer.SeoTitleEN,
                        SeoTwitterCardAR = eventVer.SeoTwitterCardAR,
                        SeoTwitterCardEN = eventVer.SeoTwitterCardEN,
                        Url = eventVer.EnUrl,
                        DetailUrl = eventVer.ArUrl
                    };

                    return eventViewModel;
                }
            }
            return null;
        }

        public IEnumerable<PageEventVersions> GetAll()
        {
            return _db.PageEventVersions.Where(x => !x.IsDeleted).ToList();
        }

        public bool ApplySubmitRequest(int id, string userId, string pageLink)
        {
            try
            {
                var eventVersion = _db.PageEventVersions.Find(id);
                var pageVer = _db.PageRouteVersions.Find(eventVersion.PageRouteVersionId);
                eventVersion.VersionStatusEnum = VersionStatusEnum.Submitted;
                _db.PageEventVersions.Update(eventVersion);
                _db.ApprovalNotifications.Add(new ApprovalNotification
                {
                    ChangeAction = eventVersion.ChangeActionEnum.Value,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    ContentManagerId = userId,
                    PageLink = $"{pageLink}/{id}?pageRouteId={pageVer.PageRouteId}",
                    PageName = "Events",
                    PageType = PageType.Static,
                    VersionStatusEnum = VersionStatusEnum.Submitted,

                });
                var draftEvents = _db.PageEventVersions.Where(d => d.PageRouteVersionId == eventVersion.PageRouteVersionId && d.Id != id &
                d.VersionStatusEnum == VersionStatusEnum.Draft).Any();
                if (!draftEvents)
                {
                    pageVer.ContentVersionStatusEnum = VersionStatusEnum.Submitted;
                    _db.PageRouteVersions.Update(pageVer);
                }
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Approve(int id, int approvalId, string userId)
        {
            var eventVer = _db.PageEventVersions.First(d => d.Id == id);
            if (eventVer != null)
            {
                var pageRouteId = _db.PageRouteVersions.Find(eventVer.PageRouteVersionId).PageRouteId;
                PageEvent pageEvent;
                if (eventVer.ChangeActionEnum == ChangeActionEnum.New)
                {
                    pageEvent = new PageEvent
                    {
                        ApprovalDate = DateTime.Now,
                        ApprovedById = userId,
                        ArDescription = eventVer.ArDescription,
                        CreatedById = eventVer.CreatedById,
                        ArTitle = eventVer.ArTitle,
                        CreationDate = eventVer.CreationDate,
                        EnDescription = eventVer.EnDescription,
                        EnTitle = eventVer.EnTitle,
                        IsActive = eventVer.IsActive,
                        IsDeleted = false,
                        PageRouteId = pageRouteId.Value,
                        ArAddress = eventVer.ArAddress,
                        ArImageAlt = eventVer.ArImageAlt,
                        ArUrl = eventVer.ArUrl,
                        EnAddress = eventVer.EnAddress,
                        EnImageAlt = eventVer.EnImageAlt,
                        EnUrl = eventVer.EnUrl,
                        EventCaption = eventVer.EventCaption,
                        EventDateColor = eventVer.EventDateColor,
                        EventEndDate = eventVer.EventEndDate,
                        EventLat = eventVer.EventLat,
                        EventLocation = eventVer.EventLocation,
                        EventLocationUrl = eventVer.EventLocationUrl,
                        EventLon = eventVer.EventLon,
                        EventSocialLinks = eventVer.EventSocialLinks,
                        EventStartDate = eventVer.EventStartDate,
                        Order = eventVer.Order,
                        SeoDescriptionAR = eventVer.SeoDescriptionAR,
                        SeoDescriptionEN = eventVer.SeoDescriptionEN,
                        SeoOgTitleAR = eventVer.SeoOgTitleAR,
                        SeoOgTitleEN = eventVer.SeoOgTitleEN,
                        SeoTitleAR = eventVer.SeoTitleAR,
                        SeoTitleEN = eventVer.SeoTitleEN,
                        SeoTwitterCardAR = eventVer.SeoTwitterCardAR,
                        SeoTwitterCardEN = eventVer.SeoTwitterCardEN,
                        ShowInHome = eventVer.ShowInHome
                    };
                    _db.PageEvents.Add(pageEvent);
                }
                else if (eventVer.ChangeActionEnum == ChangeActionEnum.Update)
                {
                    pageEvent = _db.PageEvents.Find(eventVer.PageEventId);
                    pageEvent.ApprovalDate = DateTime.Now;
                    pageEvent.ApprovedById = userId;
                    pageEvent.ArDescription = eventVer.ArDescription;
                    pageEvent.CreatedById = eventVer.CreatedById;
                    pageEvent.ArTitle = eventVer.ArTitle;
                    pageEvent.CreationDate = eventVer.CreationDate;
                    pageEvent.EnDescription = eventVer.EnDescription;
                    pageEvent.EnTitle = eventVer.EnTitle;
                    pageEvent.IsActive = eventVer.IsActive;
                    pageEvent.IsDeleted = false;
                    pageEvent.PageRouteId = pageRouteId.Value;
                    pageEvent.ArAddress = eventVer.ArAddress;
                    pageEvent.ArImageAlt = eventVer.ArImageAlt;
                    pageEvent.ArUrl = eventVer.ArUrl;
                    pageEvent.EnAddress = eventVer.EnAddress;
                    pageEvent.EnImageAlt = eventVer.EnImageAlt;
                    pageEvent.EnUrl = eventVer.EnUrl;
                    pageEvent.EventCaption = eventVer.EventCaption;
                    pageEvent.EventDateColor = eventVer.EventDateColor;
                    pageEvent.EventEndDate = eventVer.EventEndDate;
                    pageEvent.EventLat = eventVer.EventLat;
                    pageEvent.EventLocation = eventVer.EventLocation;
                    pageEvent.EventLocationUrl = eventVer.EventLocationUrl;
                    pageEvent.EventLon = eventVer.EventLon;
                    pageEvent.EventSocialLinks = eventVer.EventSocialLinks;
                    pageEvent.EventStartDate = eventVer.EventStartDate;
                    pageEvent.Order = eventVer.Order;
                    pageEvent.SeoDescriptionAR = eventVer.SeoDescriptionAR;
                    pageEvent.SeoDescriptionEN = eventVer.SeoDescriptionEN;
                    pageEvent.SeoOgTitleAR = eventVer.SeoOgTitleAR;
                    pageEvent.SeoOgTitleEN = eventVer.SeoOgTitleEN;
                    pageEvent.SeoTitleAR = eventVer.SeoTitleAR;
                    pageEvent.SeoTitleEN = eventVer.SeoTitleEN;
                    pageEvent.SeoTwitterCardAR = eventVer.SeoTwitterCardAR;
                    pageEvent.SeoTwitterCardEN = eventVer.SeoTwitterCardEN;
                    pageEvent.ShowInHome = eventVer.ShowInHome;
                    _db.PageEvents.Update(pageEvent);

                }
                else
                {
                    pageEvent = _db.PageEvents.Find(eventVer.PageEventId);
                    pageEvent.IsDeleted = true;
                    _db.PageEvents.Update(pageEvent);
                }
                _db.SaveChanges();
                eventVer.VersionStatusEnum = VersionStatusEnum.Approved;
                eventVer.PageEventId = pageEvent.Id;
                _db.PageEventVersions.Update(eventVer);

                var notification = _db.ApprovalNotifications.Find(approvalId);
                notification.VersionStatusEnum = VersionStatusEnum.Approved;
                _db.ApprovalNotifications.Update(notification);

                var submittedEvents = _db.PageEventVersions.Where(d => d.PageRouteVersionId == eventVer.PageRouteVersionId && d.Id != id &
               d.VersionStatusEnum == VersionStatusEnum.Submitted).Any();
                if (!submittedEvents)
                {
                    var pageVer = _db.PageRouteVersions.Find(eventVer.PageRouteVersionId);
                    pageVer.ContentVersionStatusEnum = VersionStatusEnum.Approved;
                    _db.PageRouteVersions.Update(pageVer);
                }
                _db.SaveChanges();

                return true;
            }
            return false;
        }

        public bool Ignore(int id, int approvalId, string userId)
        {
            var eventVer = _db.PageEventVersions.First(d => d.Id == id);
            if (eventVer != null)
            {
                var pageRouteId = _db.PageRouteVersions.Find(eventVer.PageRouteVersionId).PageRouteId;

                eventVer.VersionStatusEnum = VersionStatusEnum.Ignored;
                _db.PageEventVersions.Update(eventVer);

                var notification = _db.ApprovalNotifications.Find(approvalId);
                notification.VersionStatusEnum = VersionStatusEnum.Ignored;
                _db.ApprovalNotifications.Update(notification);

                var submittedEvents = _db.PageEventVersions.Where(d => d.PageRouteVersionId == eventVer.PageRouteVersionId && d.Id != id &
              d.VersionStatusEnum == VersionStatusEnum.Submitted).Any();
                if (!submittedEvents)
                {
                    var pageVer = _db.PageRouteVersions.Find(eventVer.PageRouteVersionId);
                    pageVer.ContentVersionStatusEnum = VersionStatusEnum.Approved;
                    _db.PageRouteVersions.Update(pageVer);
                }
                _db.SaveChanges();

                return true;
            }
            return false;
        }

        public IEnumerable<PageEvent> GetAllPageEvent()
        {
            return _db.PageEvents.Where(x => !x.IsDeleted && x.IsActive).ToList();
        }
    }
}
