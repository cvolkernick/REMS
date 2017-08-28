using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using REMS.DataAccess;
using REMS.Models;
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
            REMSDAL dal = new REMSDAL();

            Tenant t = new Tenant();
            Complex c = new Complex();
            Unit u = new Unit();
            Address a = new Address();
            ContactInfo i = new ContactInfo();

            a.Address1 = "123 Test Street";
            a.City = "Test City";
            a.State = "Test State";
            a.Zip = "12345";

            i.Address = a;
            i.Phone1 = "555-555-5555";
            i.Email = "test@email.com";

            t.FirstName = "Test";
            t.LastName = "Tenant";
            t.ContactInfo = i;

            u.Name = "Test Unit";
            u.AddTenant(t);

            c.Name = "Test Complex";
            c.Address = a;
            c.AddUnit(u);                       

            dal.Tenants.Add(t);
            dal.Addresses.Add(a);
            dal.Contacts.Add(i);

            dal.Complexes.Add(c);
            dal.Units.Add(u);

            dal.SaveChanges();

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