using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using REMS.DataAccess;
using REMS.Models;
using REMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace REMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                AdminSummaryViewModel viewModel = new AdminSummaryViewModel();

                using (REMSDAL dal = new REMSDAL())
                {
                    viewModel.OwnersCount = dal.Owners.Count();
                    viewModel.ComplexesCount = dal.Complexes.Count();
                    viewModel.StaffCount = dal.StaffMembers.Count();
                    viewModel.TenantCount = dal.Tenants.Count();
                    viewModel.UnitsCount = dal.Units.Count();
                }

                    return View(viewModel);
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private Boolean IsAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var s = userManager.GetRoles(user.GetUserId());

                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
            }

            return false;
        }
    }
}