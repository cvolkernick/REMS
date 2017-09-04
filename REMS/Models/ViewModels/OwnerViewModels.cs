using REMS.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REMS.Models.ViewModels
{
    public class OwnerViewModels
    {
    }

    public class ViewOwnersViewModel
    {
        public List<Owner> Owners { get; set; }

        [Display(Name = "ActionStatusMessageViewModel")]
        public ActionStatusViewModel ActionStatusMessageViewModel { get; set; }

        public ViewOwnersViewModel()
        {
            ActionStatusMessageViewModel = new ActionStatusViewModel();
        }
    }

    public class AddOwnerViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "ContactInfo")]
        public ContactInfo ContactInfo { get; set; }

        [Display(Name = "ActionStatusMessageViewModel")]
        public ActionStatusViewModel ActionStatusMessageViewModel { get; set; }

        public AddOwnerViewModel()
        {
            ActionStatusMessageViewModel = new ActionStatusViewModel();
        }
    }

    public class UpdateOwnerViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "ContactInfo")]
        public ContactInfo ContactInfo { get; set; }

        [Display(Name = "ActionStatusMessageViewModel")]
        public ActionStatusViewModel ActionStatusMessageViewModel { get; set; }

        public AddOwnerViewModel()
        {
            ActionStatusMessageViewModel = new ActionStatusViewModel();
        }
    }

    public class DeleteOwnerViewModel
    {

    }
}