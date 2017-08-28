using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace REMS.Models
{
    public class ContactInfo
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }

        public ContactInfo()
        {
            this.Id = System.Guid.NewGuid();
        }
    }
}