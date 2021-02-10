using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IPageSectionRepository
    {
        List<PageSection> GetApprovedPageSectionsByContentId(int contentId);
        List<PageSectionType> GetPageSectionTypes();
        PageSectionType GetPageSectionType(int id);
        //PageSection Add(DynamicPageSection dynamicPageSection);
    }
}
