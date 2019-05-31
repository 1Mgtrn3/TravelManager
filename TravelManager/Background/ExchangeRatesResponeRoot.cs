using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelManager.Background
{
    public class ExchangeRatesResponeRoot
    {
        public string @base { get; set; }
        public Rates rates { get; set; }
        public string date { get; set; }
    }
}
