using REMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REMS.DataAccess
{
    public class OwnersManager
    {
        public ICollection<Owner> GetOwners()
        {
            using (REMSDAL dal = new REMSDAL())
            {
                return dal.Owners.ToList();
            }
        }
    }
}