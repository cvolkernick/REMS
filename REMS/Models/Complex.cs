using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace REMS.Models
{
    public class Complex
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public Guid? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        public virtual ICollection<Unit> Units { get; set; }
        public virtual ICollection<StaffMember> StaffMembers { get; set; }
        public virtual ICollection<Owner> Owners { get; set; }

        public Complex()
        {
            this.Id = System.Guid.NewGuid();
            this.Units = new HashSet<Unit>();
            this.StaffMembers = new HashSet<StaffMember>();
            this.Owners = new HashSet<Owner>();
        }

        public void AddUnit(Unit unit)
        {
            Units.Add(unit);
        }

        public void AddStaff(StaffMember staffMember)
        {
            StaffMembers.Add(staffMember);
        }

        public void AddOwner(Owner owner)
        {
            Owners.Add(owner);
        }
    }
}