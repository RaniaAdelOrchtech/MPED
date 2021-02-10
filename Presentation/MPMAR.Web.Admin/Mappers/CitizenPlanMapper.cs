using MPMAR.Data.HomePageModels;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class CitizenPlanMapper
    {
        public static CitizenPlan MapToCitizenPlanModel(this CitizenPlanViewModel model)
        {
            return new CitizenPlan()
            {
                Id = model.Id,
                ArTitle = model.ArTitle,
                EnTitle = model.EnTitle,
                ArMainTitle = model.ArMainTitle,
                ArDescription = model.ArDescription,
                EnDescription = model.EnDescription,
                EnMainTitle = model.EnMainTitle,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                Image = model.Image,
                EnImage = model.EnImage,
                Link = model.Link
            };

        }

        public static CitizenPlanViewModel MapToCitizenPlanViewModel(this CitizenPlan model)
        {
            return new CitizenPlanViewModel()
            {
                Id = model.Id,
                ArTitle = model.ArTitle,
                EnTitle=model.EnTitle,
                ArMainTitle = model.ArMainTitle,
                ArDescription=model.ArDescription,
                EnDescription = model.EnDescription,
                EnMainTitle = model.EnMainTitle,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                Image = model.Image,
                EnImage = model.EnImage,
                Link = model.Link
            };

        }
    }
}
