using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelManager.Models
{
    public class Currency
    {
        public long CurrencyId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; } 
        public string Description { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<ExchangeRate> FirstExchangeRates { get; set; }
        public ICollection<ExchangeRate> SecondExchangeRates { get; set; }
    }
}
