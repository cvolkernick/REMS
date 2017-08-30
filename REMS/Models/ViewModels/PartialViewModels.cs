using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REMS.ViewModels
{
    public class PartialViewModels
    {
    }

    public class ActionStatusViewModel
    {
        public string StatusMessage { get; set; }

        public ActionStatusViewModel()
        {
            StatusMessage = null;
        }
    }

    public class AddressViewModel
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    public class AdminSummaryViewModel
    {
        [Display(Name = "OwnersCount")]
        public int OwnersCount { get; set; }

        [Display(Name = "ComplexesCount")]
        public int ComplexesCount { get; set; }

        [Display(Name = "UnitsCount")]
        public int UnitsCount { get; set; }

        [Display(Name = "StaffCount")]
        public int StaffCount { get; set; }

        [Display(Name = "TenantCount")]
        public int TenantCount { get; set; }
    }
}