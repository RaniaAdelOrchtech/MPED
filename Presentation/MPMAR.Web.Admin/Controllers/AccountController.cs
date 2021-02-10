using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MPMAR.Business;
using MPMAR.Common;
using MPMAR.Data;
using MPMAR.Entities;
using NToastNotify;

using MPMAR.Common.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using MPMAR.Business.Interfaces;

namespace MPMAR.Web.Admin.Controllers
{

    public class AccountController : Controller
    {
        private readonly ILogger<UserManagmentRepository> _logger;
        private readonly IUserManagmentRepository _userManagment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<AccountController> _eventLogger;
        private readonly IMyEmailSender _emailSender;

        private readonly IBEUsersPrivilegesRepository _bEUsersPrivilegesRepository;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserManagmentRepository userManagment, ILogger<UserManagmentRepository> logger, IToastNotification toastNotification, IEventLogger<AccountController> eventLogger, IBEUsersPrivilegesRepository bEUsersPrivilegesRepository, IMyEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userManagment = userManagment;
            _logger = logger;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _bEUsersPrivilegesRepository = bEUsersPrivilegesRepository;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Index for all accounts
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = UserRolesConst.SuperAdmin)]
        public IActionResult Index()
        {
            var result = _userManager.Users.ToList();

            return View(result);
        }

        /// <summary>
        /// get method for create new account
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = UserRolesConst.SuperAdmin)]
        public IActionResult Create()
        {
            ViewBag.Roles = _roleManager.Roles.ToList();
            return View();
        }

        /// <summary>
        /// post method for create new account
        /// </summary>
        /// <param name="userRoleViewModel">user role view model</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRolesConst.SuperAdmin)]
        public async Task<IActionResult> Create(CreateUserRoleViewModel userRoleViewModel)
        {
            var user = new ApplicationUser();

            user.UserName = userRoleViewModel.Email;
            user.Email = userRoleViewModel.Email;
            user.isFirstLogin = true;
            user.TwoFactorEnabled = false;
            //  user.EmailConfirmed = true;
            string password = userRoleViewModel.Password;
            IdentityResult chkUser = await _userManager.CreateAsync(user, password);
            if (chkUser.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);


                var confirmationLink = Url.Page("/Account/ConfirmEmail", pageHandler: null, values: new { area = "Identity", userId = user.Id, token }, protocol: Request.Scheme);
                if (userRoleViewModel.IsSuperAdmin)
                {
                    var addToRole = await _userManager.AddToRoleAsync(user, UserRolesConst.SuperAdmin);
                }
                else
                {
                    var addToRole = await _userManager.AddToRoleAsync(user, UserRolesConst.ContentManager);
                }
                await _emailSender.SendEmailAsync(user.Email, user.UserName, "Confirm your email",
                         $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>clicking here</a>.");
                await _emailSender.SendEmailAsync(user.Email, user.UserName,
                        "Email Account", "Please Check Your Email Confirmation Credential <br><br>" + "Email: "
                        + user.Email + "<br><br>" + "Password: <b>" + password + "</b>" + "<br><br> <b>Please Reset Your Password</b><br> Thanks");
                _logger.LogInformation(user.Email + " Was Created" + " by Super Admin" + " and two emails sent to " + user.Email);
                _toastNotification.AddSuccessToastMessage(user.Email + " Created Successfully");

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Super Admin > User Managment > Add", user.Email + " Created Successfully");

                if (!userRoleViewModel.IsSuperAdmin)
                {

                    AddDefaultConfig(user.Id);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (IdentityError error in chkUser.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            ViewBag.Roles = _roleManager.Roles.ToList();
            return View();
        }

        private void AddDefaultConfig(string userId)
        {
            _bEUsersPrivilegesRepository.AddDefaultPrivileges(userId);
        }

        /// <summary>
        /// get method for update account
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        [Authorize(Roles = UserRolesConst.SuperAdmin)]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            EditUserRoleViewModel dto = new EditUserRoleViewModel();
            if (id == null || user == null)
                return StatusCode(404);
            else
            {
                ViewBag.Roles = _roleManager.Roles.Select(x => x.Name).ToList();
                dto.Email = user.Email;
                dto.Id = user.Id;
                dto.IsSuperAdmin = await _userManager.IsInRoleAsync(user, UserRolesConst.SuperAdmin);
                dto.IsFirstLogin = user.isFirstLogin;
                dto.TwoFactorEnabled = user.TwoFactorEnabled;
                dto.EmailConfirmed = user.EmailConfirmed;


            }
            return View(dto);

        }

        /// <summary>
        /// post method for update account
        /// </summary>
        /// <param name="editUser">user modell</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRolesConst.SuperAdmin)]
        public async Task<IActionResult> Edit(EditUserRoleViewModel editUser)
        {
            var isEdited = await _userManagment.Edit(editUser);
            if (isEdited)
            {
                if (!editUser.IsSuperAdmin)
                {

                    AddDefaultConfig(editUser.Id);
                }
                _toastNotification.AddSuccessToastMessage(editUser.Email + " Edited Successfully");
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Super Admin > User Managment > Edit", editUser.Email + " Edited Successfully");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _toastNotification.AddErrorToastMessage("There is Error in Delete " + editUser.Email);
                _logger.LogError("There is Error in Delete " + editUser.Email);
                return View();
            }
        }

        /// <summary>
        /// method for delete account
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        [Authorize(Roles = UserRolesConst.SuperAdmin)]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
                return StatusCode(404);
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return StatusCode(404);
            return View(user);
        }

        /// <summary>
        /// get method for delete account
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = UserRolesConst.SuperAdmin)]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (id == null || user == null)
                return StatusCode(404);
            var isDeleted = await _userManagment.Delete(id);
            if (isDeleted)
            {
                _toastNotification.AddSuccessToastMessage(user.Email + " Deleted Successfully");
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Super Admin > User Managment > Delete", user.Email + " Deleted Successfully");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _toastNotification.AddErrorToastMessage("There is Error in Delete " + user.Email);
                _logger.LogError("There is Error in Delete " + user.Email);
                return View();
            }
        }

        /// <summary>
        /// method for 2FA
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> ChangeTwoFactorAuth(string id)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    return false;
                }
                else
                {
                    if (user.TwoFactorEnabled)
                    {
                        user.isFirstLogin = true;
                        user.TwoFactorEnabled = false;
                    }
                    else
                    {
                        user.isFirstLogin = true;
                        user.TwoFactorEnabled = true;
                    }
                    IdentityResult update = await _userManager.UpdateAsync(user);

                    if (update.Succeeded)
                        return true;
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }


    }
}