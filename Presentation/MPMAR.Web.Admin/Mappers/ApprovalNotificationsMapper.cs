using Abp.Extensions;
using MPMAR.Analytics.Data;
using MPMAR.Business.Services.Analytics.ViewModels;
using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MPMAR.Web.Admin.Mappers
{
    public static class ApprovalNotificationsMapper
    {
        public static ApprovalNotificationsViewModel MapToApprovalNotificationsViewModel(this ApprovalNotification model)
        {
            ApprovalNotificationsViewModel viewModel = new ApprovalNotificationsViewModel()
            {
                Id = model.Id,
                ContentManagerName = model.ContentManager.UserName,
                ChangesDate = getDate(model.ChangesDateTime),
                ChangesTime = getTime(model.ChangesDateTime),
                ChangeAction = model.ChangeAction.ToString(),
                ChangeType = model.ChangeType.ToString(),
                PageLink = model.PageLink,
                PageName = model.PageName,
                PageType = model.PageType.ToString(),
                VersionStatusEnum = model.VersionStatusEnum.ToString()
            };
            return viewModel;
        }

        public static string getDate(DateTime dateTime)
        {
            return dateTime.ToShortDateString();
        }

        public static string getTime(DateTime dateTime)
        {
            return dateTime.ToShortTimeString();
        }

    }

}
