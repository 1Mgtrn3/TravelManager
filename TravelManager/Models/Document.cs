using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelManager.Models
{
    public class Document
    {
        public long DocumentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DownloadLink { get; set; }
        public ICollection<PlaceDocument> PlaceDocuments { get; set; }
        public DocumentAsignee DocumentAsignee { get; set; }
    }
}
