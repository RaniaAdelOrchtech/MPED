using MPMAR.Business.Interfaces;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Text;
using MPMAR.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace MPMAR.Business.Services
{
    public class ApprovalNotificationsRepository : IApprovalNotificationsRepository
    {
        private readonly ApplicationDbContext _db;

        public ApprovalNotificationsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Get list of notification from database
        /// </summary>
        /// <returns>return notifications objects</returns>
        public IEnumerable<ApprovalNotification> GetAll_Index()
        {
            var notificationsData = _db.ApprovalNotifications.Where(na => na.VersionStatusEnum == VersionStatusEnum.Submitted).Include(na => na.ContentManager)
                .OrderByDescending(na => na.Id).ToList();

            return notificationsData;
        }

        /// <summary>
        /// Get list of approved and ignored notification from database
        /// </summary>
        /// <returns>return notifications objects</returns>
        public IEnumerable<ApprovalNotification> GetAll_History()
        {
            var notificationsData = _db.ApprovalNotifications.Where(na => na.VersionStatusEnum == VersionStatusEnum.Approved || na.VersionStatusEnum == VersionStatusEnum.Ignored).Include(na => na.ContentManager)
                .OrderByDescending(na => na.Id).ToList();

            return notificationsData;
        }

        /// <summary>
        /// Add new notification to approval user
        /// </summary>
        /// <param name="model">Approval notification model</param>
        /// <returns>return a single notification</returns>
        public void Add(ApprovalNotification model)
        {
            _db.ApprovalNotifications.Add(model);
            _db.SaveChanges();
        }

        /// <summary>
        /// Get specific notification by id
        /// </summary>
        /// <param name="id">Approval notification id</param>
        /// <returns>return a single notification</returns>
        public ApprovalNotification GetById(int id)
        {
            return _db.ApprovalNotifications.Find(id);
        }

        /// <summary>
        /// Get a notification with the same relatedVersionId, id not equal sent id in parameter and status is submitted
        /// </summary>
        /// <param name="prvId">page route version id</param>
        /// <param name="id">notification id</param>
        /// <returns>return a single notification</returns>
        public ApprovalNotification GetByIdRelatedId(int prvId, int id)
        {
            return _db.ApprovalNotifications.FirstOrDefault(
                n => n.RelatedVersionId == prvId &&
                n.Id != id &&
                n.VersionStatusEnum == VersionStatusEnum.Submitted);
        }

        /// <summary>
        /// Get a notification with the same relatedVersionId and with the same related page status
        /// </summary>
        /// <param name="prvId">page route version id</param>
        /// <param name="relatedPageEnum">related page status</param>
        /// <returns>return a single notification</returns>
        public ApprovalNotification GetByRelatedIdAndEnum(int prvId, RelatedPageEnum relatedPageEnum)
        {
            return _db.ApprovalNotifications.OrderByDescending(x => x.Id).FirstOrDefault(
                an => an.RelatedVersionId == prvId && an.RelatedPageEnum == relatedPageEnum &&
                an.VersionStatusEnum == VersionStatusEnum.Submitted);
        }

        /// <summary>
        /// Update a notification by new notification model
        /// </summary>
        /// <param name="approvalNotification">Approval notification model</param>
        /// <returns>return a single notification</returns>
        public void Update(ApprovalNotification approvalNotification)
        {
            _db.ApprovalNotifications.Update(approvalNotification);
            _db.SaveChanges();
        }

        /// <summary>
        /// Get a notification with the same page name
        /// </summary>
        /// <param name="pageName">page name in notification</param>
        /// <returns>return a single notification</returns>
        public ApprovalNotification GetByPageName(string pageName)
        {
            return _db.ApprovalNotifications.OrderByDescending(x => x.Id).FirstOrDefault(x => x.PageName == pageName);
        }

        /// <summary>
        /// Get a notification with the same page name and same related version id
        /// </summary>
        /// <param name="pageName">page name in notification</param>
        /// <param name="relatedId">related version id</param>
        /// <returns>return a single notification</returns>
        public ApprovalNotification GetByPageNameAndRelatedId(string pageName, int relatedId)
        {
            return _db.ApprovalNotifications.OrderByDescending(x => x.Id).FirstOrDefault(x => x.PageName == pageName && x.RelatedVersionId == relatedId);
        }
        public ApprovalNotification GetByPageNameAndChangeType(string sectionName, ChangeType changeType = ChangeType.PageContent)
        {
            return _db.ApprovalNotifications.OrderByDescending(x => x.Id).FirstOrDefault(x => x.PageName == sectionName && x.ChangeType == changeType);
        }
        /// <summary>
        /// Get a notification with the same relatedVersionId and with the same change type
        /// </summary>
        /// <param name="relatedId">related version id</param>
        /// <param name="type">change type</param>
        /// <returns>return a single notification</returns>
        public ApprovalNotification GetByRelatedIdAndType(int relatedId, ChangeType type)
        {
            return _db.ApprovalNotifications.OrderByDescending(x => x.Id).FirstOrDefault(x => x.RelatedVersionId == relatedId && x.ChangeType == type);
        }

        public void DeleteAllRelatedNotificationsToDynamicPage(int dynamicPageId)
        {
            var allRelatedDynamicPageVersions = _db.PageRouteVersions.Where(x => x.PageRouteId == dynamicPageId);
            var allRelatedNotifications = _db.ApprovalNotifications.Where(x => allRelatedDynamicPageVersions.Any(y => y.Id == x.RelatedVersionId));

            _db.ApprovalNotifications.RemoveRange(allRelatedNotifications);
            _db.SaveChanges();
        }
   
    }
}
