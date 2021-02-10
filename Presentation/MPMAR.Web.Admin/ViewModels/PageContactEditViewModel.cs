using Microsoft.AspNetCore.Http;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MPMAR.Web.Admin.ViewModels
{
    public class PageContactEditViewModel : PageSeo
    {
        public int Id { get; set; }
        public int? PageRouteVersionId { get; set; }
        //[Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string ArParticipateTitle { get; set; }
        public string EnParticipateTitle { get; set; }
        public string ArMapTitle { get; set; }
        public string EnMapTitle { get; set; }

        public string ArPageName { get; set; }
        public string EnPageName { get; set; }


        public string ArAddress { get; set; }
        public string EnAddress { get; set; }
        public bool FormParticipateActive { get; set; }
        public string MapUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string EmailParticipateEmail { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? PageContactId { get; set; }
    }

    public class PageContactListViewModel
    {
        public int Id { get; set; }
        public string ArParticipateTitle { get; set; }
        public string EnParticipateTitle { get; set; }
        public string ArPageName { get; set; }
        public string EnPageName { get; set; }

        public string ArMapTitle { get; set; }
        public string EnMapTitle { get; set; }
        public string ArAddress { get; set; }
        public string EnAddress { get; set; }
        public bool FormParticipateActive { get; set; }
        public string MapUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string EmailParticipateEmail { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
