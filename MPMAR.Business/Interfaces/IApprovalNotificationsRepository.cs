using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IApprovalNotificationsRepository
    {
        /// <summary>
        /// Get list of notification from database
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApprovalNotification> GetAll_Index();

        /// <summary>
        /// Get list of approved and ignored notification from database
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApprovalNotification> GetAll_History();

        /// <summary>
        /// Add new notification to approval user
        /// </summary>
        /// <param name="model">Approval notification model</param>
        /// <returns></returns>
        void Add(ApprovalNotification model);

        /// <summary>
        /// Get specific notification by id
        /// </summary>
        /// <param name="id">Approval notification id</param>
        /// <returns></returns>
        ApprovalNotification GetById(int id);

        /// <summary>
        /// Update a notification by new notification model
        /// </summary>
        /// <param name="approvalNotification">Approval notification model</param>
        /// <returns></returns>
        void Update(ApprovalNotification approvalNotification);

        /// <summary>
        /// Get a notification with the same relatedVersionId, id not equal sent id in parameter and status is submitted
        /// </summary>
        /// <param name="prvId">page route version id</param>
        /// <param name="id">notification id</param>
        /// <returns></returns>
        ApprovalNotification GetByIdRelatedId(int prvId, int id);

        /// <summary>
        /// Get a notification with the same relatedVersionId and with the same related page status
        /// </summary>
        /// <param name="prvId">page route version id</param>
        /// <param name="relatedPageEnum">related page status</param>
        /// <returns></returns>
        ApprovalNotification GetByRelatedIdAndEnum(int prvId, RelatedPageEnum relatedPageEnum);

        /// <summary>
        /// Get a notification with the same page name
        /// </summary>
        /// <param name="pageName">page name in notification</param>
        /// <returns></returns>
        ApprovalNotification GetByPageName(string pageName);

        /// <summary>
        /// Get a notification with the same relatedVersionId and with the same change type
        /// </summary>
        /// <param name="relatedId">related version id</param>
        /// <param name="type">change type</param>
        /// <returns></returns>
        ApprovalNotification GetByRelatedIdAndType(int relatedId, ChangeType type);

        /// <summary>
        /// Get a notification with the same page name and same related version id
        /// </summary>
        /// <param name="pageName">page name in notification</param>
        /// <param name="relatedId">related version id</param>
        /// <returns></returns>
        ApprovalNotification GetByPageNameAndRelatedId(string pageName, int relatedId);
        ApprovalNotification GetByPageNameAndChangeType(string sectionName, ChangeType changeType=ChangeType.PageContent);
        void DeleteAllRelatedNotificationsToDynamicPage(int dynamicPageId);
      
    }
}
