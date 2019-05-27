using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelManager.Models;

namespace TravelManager.Helpers
{
    public class CurrencyConverter
    {
        //public CurrencyConverter()
        //{

        //}
        private readonly TravelManagerContext _context;

        public CurrencyConverter(TravelManagerContext context)
        {
            _context = context;
        }

        public double Convert(double amount, long inputCurrencyId, long outputCurrencyId) {

            double rate = _context.ExchangeRates.SingleOrDefault(c => (c.FirstCurrencyId == inputCurrencyId && c.SecondCurrencyId == outputCurrencyId)).Rate;
            if (rate == default(double))
            {
                rate = _context.ExchangeRates.SingleOrDefault(c => c.FirstCurrencyId == outputCurrencyId && c.SecondCurrencyId == inputCurrencyId).Rate;
                if (rate == default(double))
                {
                    return amount;
                }
                rate = 1 / rate;    
            }


            return amount * rate;


            //return 0;
        }
    }
}
