using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace REMS.Models
{
    public class Unit
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
                
        public Guid ComplexId { get; set; }
        [ForeignKey("ComplexId")]
        public virtual Complex Complex { get; set; }

        public virtual ICollection<Tenant> Tenants { get; set; }

        public Unit()
        {
            this.Id = System.Guid.NewGuid();
            this.Tenants = new HashSet<Tenant>();
        }

        public void AddTenant(Tenant tenant)
        {
            Tenants.Add(tenant);
        }
    }
}