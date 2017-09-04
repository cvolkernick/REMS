using REMS.DataAccess;
using REMS.Models;
using REMS.Models.ViewModels;
using REMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace REMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OwnersController : Controller
    {
        private OwnersManager ownerManager;

        public OwnersController()
        {
            ownerManager = new OwnersManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewOwners()
        {
            ViewOwnersViewModel viewModel = new ViewOwnersViewModel();
            viewModel.Owners = ownerManager.GetOwners().ToList();

            return View(viewModel);

        }

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

        public async Task<ActionResult> UpdateOwner(Guid ownerId)
        {
            using (REMSDAL dal = new REMSDAL())
            {
                var selectedOwner = dal.Owners.FirstOrDefault(o => o.Id == ownerId);
            }

            UpdateViewModel viewModel = new UpdateViewModel();


            return View(viewModel);
        }
    }
}