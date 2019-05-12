using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelManager.Models
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public DocumentAsignee DocumentAsignee { get; set; }
        public RoleAsignee RoleAsignee { get; set; }
    }
}
