using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business;
using MPMAR.Common;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.AuthRequirement;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Web.Admin.ViewModels;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class ContactUSController : Controller
    {
        private readonly IContactUsRepository _contactUSRepository;
        public ContactUSController(IContactUsRepository contactUSRepository)
        {
            _contactUSRepository = contactUSRepository;
        }


        /// <summary>
        /// grid contact us in index
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ContactUs, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// get all contact us objects
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ContactUs, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetContactData()
        {
            var data = _contactUSRepository.Get();
            var newItems = new List<ContactUsViewModel>();
            foreach (var item in data)
            {
                var newItem = new ContactUsViewModel()
                {
                    CreationDate = item.CreationDate.ToShortDateString(),
                    Address = item.Address,
                    City = item.City,
                    Email = item.Email,
                    FirstName = item.FirstName,
                    Id = item.Id,
                    Message = item.Message,
                    PhoneNumber = item.PhoneNumber,
                    PostalNumber = item.PostalNumber,
                    Region = item.Region,
                    SecondName = item.SecondName,
                    Topic = item.Topic,
                };

                newItems.Add(newItem);
            }
            return Json(new { data = newItems });
        }
    }
}