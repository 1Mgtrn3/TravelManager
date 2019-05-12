using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelManager.Models
{
    public class Role
    {
        public long RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PlaceRole> PlaceRoles { get; set; }
        public RoleAsignee RoleAsignee { get; set; }
    }
}
