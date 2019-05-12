using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelManager.Models
{
    public class RoleAsignee
    {
        public long RoleAsigneeId { get; set; }
        public long RoleId { get; set; }
        public long EmployeeId {get;set;}
        public Event Event { get; set; }
        public Role Role { get; set; }
        public long EventId { get; set; }
        public Employee Employee { get; set; }
    }
}
