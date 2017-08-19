using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REMS.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public UserType UserType { get; set; }


    }
}