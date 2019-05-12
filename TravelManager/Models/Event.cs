using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelManager.Models
{
    public class Event
    {
        public long EventId { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Partners { get; set; }
        public string Location { get; set; }
        public Double Cost { get; set; }
        public bool Planned { get; set; }

        public long CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public long PlaceId { get; set; }
        public Place Place { get; set; }

        public ICollection<RoleAsignee> RoleAsignees { get; set; }
        public ICollection<DocumentAsignee> DocumentAsignees { get; set; }
    }
}
