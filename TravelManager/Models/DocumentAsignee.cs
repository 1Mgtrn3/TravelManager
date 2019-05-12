using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelManager.Models
{
    public class DocumentAsignee
    {
        public long DocumentAsigneeId { get; set; }
        public long DocumentId { get; set; }
        public long EmployeeId { get; set; }
        public Document Document { get; set; }
        public Employee Employee { get; set; }
        public long EventId { get; set; }
        public Event Event { get; set; }
    }
}
