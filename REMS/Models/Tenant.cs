using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace REMS.Models
{
    public class Tenant
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public Guid ContactInfoId { get; set; }
        [ForeignKey("ContactInfoId")]
        public ContactInfo ContactInfo { get; set; }
        
        //public virtual ICollection<Unit> Units { get; set; }

        public Tenant()
        {
            this.Id = System.Guid.NewGuid();
            //this.Units = new HashSet<Unit>();
        }
    }
}