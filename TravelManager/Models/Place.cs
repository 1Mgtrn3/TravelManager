using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelManager.Models
{
    public class Place
    {
        public long PlaceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public byte Priority { get; set; }
        public string Website { get; set; }
        public ICollection<PlaceDocument> PlaceDocuments { get; set; }
        public ICollection<PlaceRole> PlaceRoles { get; set; }
        public ICollection<Event> Events { get; set; }

    }
}
