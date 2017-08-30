using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using REMS.DataAccess;
using REMS.Models;
using REMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        ApplicationDbContext identityContext;     

        public AdminController()
        {
            identityContext = new ApplicationDbContext();            
        }
        #region IdentityFunctions
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
#endregion

        public ActionResult Index()
        {
            return View();
        }

        #region UserFunctions
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
        #endregion

        #region StaffFunctions

        public ActionResult AddStaff()
        {
            AddStaffViewModel viewModel = new AddStaffViewModel();
            viewModel.FirstName = "";
            viewModel.LastName = "";

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddStaff(AddStaffViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (REMSDAL dal = new REMSDAL())
                {
                    StaffMember staff = new StaffMember();
                    staff.FirstName = viewModel.FirstName;
                    staff.LastName = viewModel.LastName;

                    dal.StaffMembers.Add(staff);

                    var result = await dal.SaveChangesAsync();

                    if (result > 0)
                    {
                        viewModel.ActionStatusMessageViewModel.StatusMessage = "Staff Member " + viewModel.FirstName + " " + viewModel.LastName + " added.";
                        viewModel.FirstName = "";
                        viewModel.LastName = "";

                        return View(viewModel);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
                        
            viewModel.ActionStatusMessageViewModel.StatusMessage = "There was an issue processing your request.";
            viewModel.FirstName = "";
            viewModel.LastName = "";

            return View(viewModel);
        }

        public ActionResult AssignStaff()
        {
            AssignStaffViewModel viewModel = new AssignStaffViewModel();
            viewModel.Users = GetUserNames();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignStaff(AssignStaffViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (REMSDAL dal = new REMSDAL())
                {
                    var user = UserManager.Users.SingleOrDefault(u => u.UserName == viewModel.UserName);
                    var staffMember = dal.StaffMembers.SingleOrDefault(s => s.Id == viewModel.StaffId);

                    staffMember.UserId = user.UserName;

                    var result = await dal.SaveChangesAsync();



                    // ^^^ NEW CODE ^^^

                    if (result > 0)
                    {
                        viewModel.ActionStatusMessageViewModel.StatusMessage = "Staff member " + staffMember.FirstName + staffMember.LastName + " updated.";
                        viewModel.Users = GetUserNames();
                        //viewModel.StaffMembers = dal.StaffMembers.SingleOrDefault(s => s.Id == viewModel.StaffId);

                        return View(viewModel);
                    }
                }
            }

            // If we got this far, something failed, redisplay form

            viewModel.Users = GetUserNames();
            viewModel.ActionStatusMessageViewModel.StatusMessage = "There was an issue processing your request.";

            return View(viewModel);
        }
        #endregion

        #region PropertyFunctions
        public ActionResult AddComplex()
        {
            AddComplexViewModel viewModel = new AddComplexViewModel();
            viewModel.Owners = GetOwners();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddComplex(AddComplexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (REMSDAL dal = new REMSDAL())
                {
                    var owner = GetOwner(viewModel.SelectedOwner);
                    dal.Owners.Attach(owner);

                    Complex complex = new Complex();
                    complex.Name = viewModel.Name;

                    Address address = new Address();
                    address.Address1 = viewModel.AddressViewModel.Address1;
                    address.Address2 = viewModel.AddressViewModel.Address2;
                    address.City = viewModel.AddressViewModel.City;
                    address.State = viewModel.AddressViewModel.State;
                    address.Zip = viewModel.AddressViewModel.Zip;

                    complex.Address = address;
                    complex.AddressId = address.Id;
                    complex.AddOwner(owner);
                    
                    dal.Complexes.Add(complex);
                                
                    var result = await dal.SaveChangesAsync();

                    if (result > 0)
                    {
                        viewModel.ActionStatusMessageViewModel.StatusMessage = "Complex " + viewModel.Name + " added.";
                        viewModel.Owners = GetOwners();

                        return View(viewModel);
                    }
                }           
            }

            // If we got this far, something failed, redisplay form

            viewModel.Owners = GetOwners();
            viewModel.ActionStatusMessageViewModel.StatusMessage = "There was an issue processing your request.";

            return View(viewModel);
        }

        public ActionResult AddUnit()
        {
            AddUnitViewModel viewModel = new AddUnitViewModel();
            viewModel.Name = "";

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUnit(AddUnitViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (REMSDAL dal = new REMSDAL())
                {
                    Unit unit = new Unit();
                    unit.Name = viewModel.Name;

                    dal.Units.Add(unit);

                    var result = await dal.SaveChangesAsync();

                    if (result > 0)
                    {
                        viewModel.ActionStatusMessageViewModel.StatusMessage = "Unit " + viewModel.Name + " added.";
                        viewModel.Name = "";

                        return View(viewModel);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
                        
            viewModel.ActionStatusMessageViewModel.StatusMessage = "There was an issue processing your request.";
            viewModel.Name = "";

            return View(viewModel);
        }

        public ActionResult AddTenant()
        {
            AddTenantViewModel viewModel = new AddTenantViewModel();
            viewModel.FirstName = "";
            viewModel.LastName = "";

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTenant(AddTenantViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Tenant tenant = new Tenant();
                ContactInfo newContactInfo = new ContactInfo();
                Address newAddress = new Address();

                newAddress.Address1 = viewModel.ContactInfo.Address.Address1;
                newAddress.Address2 = viewModel.ContactInfo.Address.Address2;
                newAddress.City = viewModel.ContactInfo.Address.City;
                newAddress.State = viewModel.ContactInfo.Address.State;
                newAddress.Zip = viewModel.ContactInfo.Address.Zip;

                newContactInfo.Address = newAddress;
                newContactInfo.Email = viewModel.ContactInfo.Email;
                newContactInfo.Phone1 = viewModel.ContactInfo.Phone1;
                newContactInfo.Phone2 = viewModel.ContactInfo.Phone2;

                tenant.FirstName = viewModel.FirstName;
                tenant.LastName = viewModel.LastName;
                tenant.ContactInfo = newContactInfo;

                using (REMSDAL dal = new REMSDAL())
                {
                    dal.Tenants.Add(tenant);

                    var result = await dal.SaveChangesAsync();

                    if (result > 0)
                    {
                        viewModel.ActionStatusMessageViewModel.StatusMessage = "Tenant " + viewModel.FirstName + " " + viewModel.LastName + " added.";
                        viewModel.FirstName = "";
                        viewModel.LastName = "";

                        return View(viewModel);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
                        
            viewModel.ActionStatusMessageViewModel.StatusMessage = "There was an issue processing your request.";
            viewModel.FirstName = "";
            viewModel.LastName = "";

            return View(viewModel);
        }
        #endregion

        #region ManagementFunctions

        public ActionResult AddOwner()
        {
            AddOwnerViewModel viewModel = new AddOwnerViewModel();
            viewModel.Name = "";

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddOwner(AddOwnerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Owner newOwner = new Owner();
                ContactInfo newContactInfo = new ContactInfo();
                Address newAddress = new Address();

                newAddress.Address1 = viewModel.ContactInfo.Address.Address1;
                newAddress.Address2 = viewModel.ContactInfo.Address.Address2;
                newAddress.City = viewModel.ContactInfo.Address.City;
                newAddress.State = viewModel.ContactInfo.Address.State;
                newAddress.Zip = viewModel.ContactInfo.Address.Zip;

                newContactInfo.Address = newAddress;
                newContactInfo.Email = viewModel.ContactInfo.Email;
                newContactInfo.Phone1 = viewModel.ContactInfo.Phone1;
                newContactInfo.Phone2 = viewModel.ContactInfo.Phone2;

                newOwner.Name = viewModel.Name;
                newOwner.ContactInfo = newContactInfo;

                using (REMSDAL dal = new REMSDAL())
                {
                    dal.Owners.Add(newOwner);

                    var result = await dal.SaveChangesAsync();

                    if (result > 0)
                    {
                        viewModel.ActionStatusMessageViewModel.StatusMessage = "Owner " + viewModel.Name + " added.";
                        viewModel.Name = "";

                        return View(viewModel);
                    }
                }
            }

            // If we got this far, something failed, redisplay form

            viewModel.Name = "";
            viewModel.ActionStatusMessageViewModel.StatusMessage = "There was an issue processing your request.";

            return View(viewModel);
        }

        #endregion

        #region PrivateHelpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private SelectList GetUserRoles()
        {
            return new SelectList(identityContext.Roles.Where(u => !u.Name.Contains("Admin")).ToList(), "Name", "Name");
        }

        private SelectList GetUserNames()
        {
            var currentUserName = User.Identity.Name;

            return new SelectList(identityContext.Users.Where(u => !u.UserName.Contains(currentUserName)).ToList(), "UserName", "UserName");
        }

        private ApplicationUser GetUser(string userName)
        {
            return UserManager.Users.SingleOrDefault(u => u.UserName == userName);
        }

        private SelectList GetOwners()
        {
            using (REMSDAL dal = new REMSDAL())
            {
                return new SelectList(dal.Owners.ToList(), "Id", "Name");
            }                
        }

        private Owner GetOwner(Guid ownerId)
        {
            using (REMSDAL dal = new REMSDAL())
            {
                return dal.Owners.FirstOrDefault(o => o.Id == ownerId);
            }                
        }
        #endregion
    }
}