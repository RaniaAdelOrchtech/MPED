using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Web.Site.ViewModels;
using NToastNotify;
namespace MPMAR.Web.Site.Controllers
{
    public class ContactUsController : BaseController
    {
        private readonly IContactUsRepository _contactUsRepository;
        private readonly IToastNotification _toastNotification;
        private readonly ApplicationDbContext _dataAccessService;
        private readonly IPageRouteRepository _pageRouteRepository;
        private readonly IMyEmailSender _myEmailSender;

        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";
        public ContactUsController(IContactUsRepository contactUsRepository, IToastNotification toastNotification, ApplicationDbContext dataAccessService, IPageRouteRepository pageRouteRepository, IMyEmailSender myEmailSender)
        {
            _contactUsRepository = contactUsRepository;
            _toastNotification = toastNotification;
            _dataAccessService = dataAccessService;
            _pageRouteRepository = pageRouteRepository;
            _toastNotification = toastNotification;
            _myEmailSender = myEmailSender;
        }
        /// <summary>
        /// get contactUs index page
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(string lang)
        {
            var contactUs = _dataAccessService.PageContact.FirstOrDefault();
            var pageRoute = new PageRoute()
            {
                SeoDescriptionAR = contactUs.SeoDescriptionAR,
                SeoDescriptionEN = contactUs.SeoDescriptionEN,
                SeoOgTitleAR = contactUs.SeoOgTitleAR,
                SeoOgTitleEN = contactUs.SeoOgTitleEN,
                SeoTitleAR = contactUs.SeoTitleAR,
                SeoTitleEN = contactUs.SeoTitleEN,
                SeoTwitterCardAR = contactUs.SeoTwitterCardAR,
                SeoTwitterCardEN = contactUs.SeoTwitterCardEN,

            };
            if (contactUs == null)
            {
                return View("Error");
            }
            //ViewContactUs
            if (TempData[notificationMessageKey] != null)
            {
                switch (TempData[notificationTypeKey])
                {
                    case notificationSuccess:
                        _toastNotification.AddSuccessToastMessage(TempData[notificationMessageKey].ToString());
                        break;
                    case notificationWarning:
                        _toastNotification.AddWarningToastMessage(TempData[notificationMessageKey].ToString());
                        break;
                    case notificationError:
                        _toastNotification.AddErrorToastMessage(TempData[notificationMessageKey].ToString());
                        break;
                }
            }
            var items = _dataAccessService.PageContact.Where(i => i.IsDeleted != true && i.IsActive == true).OrderBy(i => i.Id).First();


            SetUpSEO(lang, pageRoute);
            if (lang == null || lang.ToLower() == "ar")
            {

                ViewBag.Title = contactUs.ArPageName;

            }
            else
            {
                ViewBag.Title = contactUs.EnPageName;

            }
            return View(items.MapToPageContact());
        }
        /// <summary>
        /// post data from contactUs page
        /// </summary>
        /// <param name="contactUs">ViewContactUs view model for contactus model</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Index(ViewContactUs contactUs, string lang)
        {

            var contactUsModel = _dataAccessService.PageContact.FirstOrDefault();
            if (lang == null || lang.ToLower() == "ar")
            {
                ViewBag.Title = contactUsModel.ArPageName;

            }
            else
            {
                ViewBag.Title = contactUsModel.ArPageName;

            }
            var items = _dataAccessService.PageContact.Where(i => i.IsDeleted != true && i.IsActive == true).OrderBy(i => i.Id).First();

            if (ModelState.IsValid)
            {

                ContactUs contact = _contactUsRepository.Add(contactUs.MapToPageViewContact());
                if (contact != null)
                {

                    var sentMail = await _myEmailSender.SendEmailAsync(contactUsModel.EmailParticipateEmail, contactUsModel.EmailParticipateEmail, contact.Topic ?? "", contact.Message ?? "");

                    if (sentMail)
                    {
                        var msg = lang == "en" ? "Submitted Successfully" : "تم الارسال";
                        _toastNotification.AddSuccessToastMessage(msg);
                        ModelState.Clear();
                        return View(items.MapToPageContact());
                    }
                    var msg2 = lang == "en" ? "Failed to Submit" : "حدث خطأ فل الارسال";
                    _toastNotification.AddErrorToastMessage(msg2);
                    ModelState.Clear();
                    return View(items.MapToPageContact());
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("See Validation Errors");
                    ModelState.Clear();
                    return View(items.MapToPageContact());
                }
            }
            _toastNotification.AddErrorToastMessage("See Validation Errors");
            ModelState.Clear();
            return View(items.MapToPageContact());
        }
    }
}