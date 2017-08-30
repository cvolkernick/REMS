using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REMS.Models
{
    public class StaffMember
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Owner> Employers { get; set; }

        public StaffMember()
        {
            this.Id = System.Guid.NewGuid();
            this.Employers = new HashSet<Owner>();
        }

        public void AddEmployer(Owner employer)
        {
            Employers.Add(employer);
        }
    }
}