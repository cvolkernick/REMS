using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using REMS.Models;
using REMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace REMS.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext context;        

        public AdminController()
        {
            context = new ApplicationDbContext();            
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult AddUser()
        {
            AddViewModel viewModel = new AddViewModel();
            viewModel.UserRoles = GetUserRoles();

            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(AddViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = viewModel.UserName, Email = viewModel.Email };
                var result = await UserManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    await this.UserManager.AddToRoleAsync(user.Id, viewModel.SelectedRole);

                    viewModel.ActionStatusMessageViewModel.StatusMessage = "User " + viewModel.UserName + " added.";
                    viewModel.UserRoles = GetUserRoles();

                    return View(viewModel);
                }
                
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            
            viewModel.UserRoles = GetUserRoles();
            viewModel.ActionStatusMessageViewModel.StatusMessage = "There was an issue processing your request.";

            return View(viewModel);
        }

        public ActionResult DeleteUser()
        {
            DeleteViewModel viewModel = new DeleteViewModel();
            viewModel.Users = GetUserNames();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUser(DeleteViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.Users.SingleOrDefault(u => u.UserName == viewModel.UserName);
                var result = await UserManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    viewModel.ActionStatusMessageViewModel.StatusMessage = "User " + viewModel.UserName + " deleted.";
                    viewModel.Users = GetUserNames();

                    return View(viewModel);
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form

            viewModel.Users = GetUserNames();            
            viewModel.ActionStatusMessageViewModel.StatusMessage = "There was an issue processing your request.";

            return View(viewModel);
        }

        public ActionResult UpdateUser()
        {
            UpdateViewModel viewModel = new UpdateViewModel();
            viewModel.UserNames = GetUserNames();
            viewModel.Mode = "Selecting";
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUserSelect(UpdateViewModel viewModel)
        {
            var selectedUser = GetUser(viewModel.UserName);

            viewModel.Mode = "Updating";
            viewModel.Email = selectedUser.Email;
            viewModel.UserName = selectedUser.UserName;

            return View("UpdateUser", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateUser(UpdateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = GetUser(viewModel.UserName);

                user.Email = viewModel.Email;
                user.UserName = viewModel.UserName;
                
                var result = await UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    viewModel.ActionStatusMessageViewModel.StatusMessage = viewModel.UserName + " updated.";
                    viewModel.Mode = "Selecting";
                    viewModel.UserNames = GetUserNames();

                    return View(viewModel);
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form

            viewModel.UserNames = GetUserNames();
            viewModel.ActionStatusMessageViewModel.StatusMessage = "There was an issue processing your request.";

            return View(viewModel);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private SelectList GetUserRoles()
        {
            return new SelectList(context.Roles.Where(u => !u.Name.Contains("Admin")).ToList(), "Name", "Name");
        }

        private SelectList GetUserNames()
        {
            var currentUserName = User.Identity.Name;

            return new SelectList(context.Users.Where(u => !u.UserName.Contains(currentUserName)).ToList(), "UserName", "UserName");
        }

        private ApplicationUser GetUser(string userName)
        {
            return UserManager.Users.SingleOrDefault(u => u.UserName == userName);
        }
    }
}