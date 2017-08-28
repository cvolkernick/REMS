using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace REMS.ViewModels
{
    public class AdminViewModels
    {
        
    }

    public class AddViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "SelectedRole")]
        public string SelectedRole { get; set; }
        
        [Display(Name = "UserRoles")]
        public SelectList UserRoles { get; set; }

        [Display(Name = "ActionStatusMessageViewModel")]
        public ActionStatusViewModel ActionStatusMessageViewModel { get; set; }

        public AddViewModel()
        {
            ActionStatusMessageViewModel = new ActionStatusViewModel();
        }
    }

    public class DeleteViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Display(Name = "Users")]
        public SelectList Users { get; set; }

        [Display(Name = "ActionStatusMessageViewModel")]
        public ActionStatusViewModel ActionStatusMessageViewModel { get; set; }

        public DeleteViewModel()
        {
            ActionStatusMessageViewModel = new ActionStatusViewModel();
        }
    }

    public class UpdateViewModel
    {           
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Display(Name = "UserNames")]
        public SelectList UserNames { get; set; }

        [Display(Name = "ActionStatusMessageViewModel")]
        public ActionStatusViewModel ActionStatusMessageViewModel { get; set; }

        [Display(Name = "Mode")]
        public string Mode { get; set; }

        public UpdateViewModel()
        {
            ActionStatusMessageViewModel = new ActionStatusViewModel();
        }
    }
}