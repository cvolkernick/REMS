using System;
using System.Collections.Generic;
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
}