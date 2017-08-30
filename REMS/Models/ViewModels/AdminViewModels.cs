using REMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace REMS.ViewModels
{
    public class AdminViewModels { }

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

    public class AssignStaffViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Display(Name = "Users")]
        public SelectList Users { get; set; }

        [Display(Name = "ActionStatusMessageViewModel")]
        public ActionStatusViewModel ActionStatusMessageViewModel { get; set; }

        [Required]
        [Display(Name = "StaffId")]
        public Guid StaffId { get; set; }

        [Display(Name = "StaffMembers")]
        public SelectList StaffMembers { get; set; }

        public AssignStaffViewModel()
        {
            ActionStatusMessageViewModel = new ActionStatusViewModel();
        }
    }

    public class AddComplexViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "AddressViewModel")]
        public AddressViewModel AddressViewModel { get; set; }

        [Display(Name = "SelectedOwner")]
        public Guid SelectedOwner { get; set; }
        
        [Display(Name = "Owners")]
        public SelectList Owners { get; set; }

        [Display(Name = "ActionStatusMessageViewModel")]
        public ActionStatusViewModel ActionStatusMessageViewModel { get; set; }

        public AddComplexViewModel()
        {
            ActionStatusMessageViewModel = new ActionStatusViewModel();
            AddressViewModel = new AddressViewModel();
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
}