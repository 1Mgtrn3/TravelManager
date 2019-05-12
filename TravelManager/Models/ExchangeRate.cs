using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelManager.Models
{
    public class ExchangeRate
    {
        public long ExchangeRateId { get; set; }
        public long FirstCurrencyId { get; set; }
        public Currency FirstCurrency { get; set; }
        public long SecondCurrencyId { get; set; }
        public Currency SecondCurrency { get; set; }
    
        public double Rate { get; set; }
    }
}
