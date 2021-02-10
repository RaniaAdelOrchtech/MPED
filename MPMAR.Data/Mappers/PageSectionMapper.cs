using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Data.Mappers
{
    public static class PageSectionMapper
    {
        public static PageSectionVersion MapToPageSectionVersion(this PageSection model)
        {
            PageSectionVersion pageSectionVersion = new PageSectionVersion()
            {
                Id = model.PageSectionVersionId.Value,
                ApprovalDate = model.ApprovalDate,
                ApprovedById = model.ApprovedById,
                EnTitle = model.EnTitle,
                ArTitle = model.ArTitle,
                EnDescription = model.EnDescription,
                ArDescription = model.ArDescription,
                EnImageAlt = model.EnImageAlt,
                ArImageAlt = model.ArImageAlt,
                Url = model.Url,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                Order = model.Order,
                CreationDate = model.CreationDate,
                CreatedById = model.CreatedById,
                PageSection = model
            };

            return pageSectionVersion;
        }
    }
}
