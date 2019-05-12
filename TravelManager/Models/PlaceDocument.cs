using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelManager.Models
{
    public class PlaceDocument
    {
        public long PlaceId { get; set; }
        public Place Place { get; set; }
        public long DocumentId { get; set; }
        public Document Document { get; set; }

    }
}
