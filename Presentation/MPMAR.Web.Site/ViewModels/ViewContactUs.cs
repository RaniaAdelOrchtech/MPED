using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewModels
{
    public class ViewContactUs
    {
        public int Id { get; set; }
        public string ArParticipateTitle { get; set; }
        public string EnParticipateTitle { get; set; }
        public string ArMapTitle { get; set; }
        public string EnMapTitle { get; set; }
        public string ArAddress { get; set; }
        public string EnAddress { get; set; }
        public bool FormParticipateActive { get; set; }
        public string MapUrl { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string EmailParticipateEmail { get; set; }

       
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
    
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
   
        public string Region { get; set; }
        [Required] 
        public string PostalNumber { get; set; }
        
        public string UserPhoneNumber { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
    }
    public static class PageContactMapper
    {
        public static ViewContactUs  MapToPageContact(this PageContact pageContactCreateViewModel)
        {
            ViewContactUs pageContact = new ViewContactUs();
            pageContact.EnMapTitle = pageContactCreateViewModel.EnMapTitle;
            pageContact.ArMapTitle = pageContactCreateViewModel.ArMapTitle;
            pageContact.ArParticipateTitle = pageContactCreateViewModel.ArParticipateTitle;
            pageContact.EnParticipateTitle = pageContactCreateViewModel.EnParticipateTitle;
            pageContact.ArAddress = pageContactCreateViewModel.ArAddress;
            pageContact.EnAddress = pageContactCreateViewModel.EnAddress;
            pageContact.MapUrl = pageContactCreateViewModel.MapUrl;
            pageContact.PhoneNumber = pageContactCreateViewModel.PhoneNumber;
            pageContact.FaxNumber = pageContactCreateViewModel.FaxNumber;
            pageContact.EmailParticipateEmail = pageContactCreateViewModel.EmailParticipateEmail;
            pageContact.FormParticipateActive = pageContactCreateViewModel.FormParticipateActive;


            
            return pageContact;
        }

        public static ContactUs MapToPageViewContact(this ViewContactUs pageContactCreateViewModel)
        {
            ContactUs pageContact = new ContactUs();
            pageContact.Email = pageContactCreateViewModel.Email;
            pageContact.FirstName = pageContactCreateViewModel.FirstName;
            pageContact.SecondName = pageContactCreateViewModel.SecondName;
            pageContact.Address = pageContactCreateViewModel.Address;
            pageContact.City = pageContactCreateViewModel.City;
            pageContact.Region = pageContactCreateViewModel.Region;
            pageContact.PostalNumber = pageContactCreateViewModel.PostalNumber;
            pageContact.PhoneNumber = pageContactCreateViewModel.PhoneNumber;
            pageContact.Topic = pageContactCreateViewModel.Topic;
            pageContact.Message = pageContactCreateViewModel.Message;


         

            return pageContact;
        }
    }


    public static class DateTimeDayOfMonthExtensions
    {
        public static DateTime FirstDayOfMonth_AddMethod(this DateTime value)
        {
            return value.Date.AddDays(1 - value.Day);
        }

        public static DateTime FirstDayOfMonth_NewMethod(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static DateTime LastDayOfMonth_AddMethod(this DateTime value)
        {
            return value.FirstDayOfMonth_AddMethod().AddMonths(1).AddDays(-1);
        }

        public static DateTime LastDayOfMonth_AddMethodWithDaysInMonth(this DateTime value)
        {
            return value.Date.AddDays(DateTime.DaysInMonth(value.Year, value.Month) - value.Day);
        }

        public static DateTime LastDayOfMonth_SpecialCase(this DateTime value)
        {
            return value.AddDays(DateTime.DaysInMonth(value.Year, value.Month) - 1);
        }

        public static int DaysInMonth(this DateTime value)
        {
            return DateTime.DaysInMonth(value.Year, value.Month);
        }

        public static DateTime LastDayOfMonth_NewMethod(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, DateTime.DaysInMonth(value.Year, value.Month));
        }

        public static DateTime LastDayOfMonth_NewMethodWithReuseOfExtMethod(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.DaysInMonth());
        }
    }


}
