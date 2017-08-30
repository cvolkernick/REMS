using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace REMS.Models
{
    public class Owner
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid? ContactInfoId { get; set; }
        [ForeignKey("ContactInfoId")]
        public ContactInfo ContactInfo { get; set; }

        public virtual ICollection<StaffMember> Employees { get; set; }
        public virtual ICollection<Complex> Complexes { get; set; }

        public Owner()
        {
            this.Id = System.Guid.NewGuid();
            this.Employees = new HashSet<StaffMember>();
            this.Complexes = new HashSet<Complex>();
        }
        
        public void AddEmployee(StaffMember employee)
        {
            Employees.Add(employee);
        }

        public void AddComplex(Complex complex)
        {
            Complexes.Add(complex);
        }
    }
}