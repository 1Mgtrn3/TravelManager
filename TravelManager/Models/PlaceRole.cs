using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelManager.Models
{
    public class PlaceRole
    {
        public long PlaceId {get;set;}
        public Place Place { get; set; }
        public long RoleId { get; set; }
        public Role Role { get; set; }
    }
}
