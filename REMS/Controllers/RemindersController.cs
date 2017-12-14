using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace REMS.Controllers
{
    public class RemindersController : Controller
    {
        // GET: Reminders
        public ActionResult Index()
        {
            return View();
        }
    }
}