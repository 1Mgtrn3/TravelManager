using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TravelManager.Models;

namespace TravelManager.Background
{
    public interface IExchangeRateProvider {
        Task Start();
    }
    public class ExchangeRateProvider : IExchangeRateProvider
    {
        private readonly TravelManagerContext _context;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        private Uri BaseEndpoint { get; set; }
        public ExchangeRateProvider(TravelManagerContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            BaseEndpoint = new Uri(_configuration.GetValue<string>("ExchangeRatesAPIurl"), UriKind.Absolute);
            _httpClient = new HttpClient();
            
        }


        public async Task Start()
        {
           var ExRatesRAW = await GetDataAsync(BaseEndpoint);
           var RatesList = ExtractRates(ExRatesRAW);
           await PushToDB(RatesList);
        }
        public List<KeyValuePair<string, double>> ExtractRates(string ExRatesRAW) {

            var resource = JObject.Parse(ExRatesRAW);
            string ratesNode = "rates";
            List<KeyValuePair<string, double>> RatesList = new List<KeyValuePair<string, double>>();
            foreach (var property in resource.Properties())
            {
                

                if (property.Name == ratesNode)
                {
                    foreach (var item in property.Value.AsJEnumerable())
                    {

                        //Console.WriteLine("Item: " + item);
                        var itemPath = item.Path;
                        //Console.WriteLine($"item path: " + itemPath);
                        string itemName = itemPath.Substring(ratesNode.Length + 1);
                        
                        string itemValue = item.ToString().Substring(itemName.Length + 1);
                        
                        double resultValue;

                        Double.TryParse(itemValue,out resultValue);

                        if (resultValue !=0)
                        {
                            RatesList.Add(new KeyValuePair<string, double>(itemName, resultValue));
                        }
                        
                    }
                }
            }

            return RatesList;
        }
        
        public async Task<string> GetDataAsync(Uri requestUrl)
        {

            
            var response = await _httpClient.GetAsync(BaseEndpoint, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            
            return data;
        }

        private async Task PushToDB(List<KeyValuePair<string, double>> RatesList) {
            

           


            Dictionary<string, long> CurrencyCodeIds = new Dictionary<string, long>();

            List<string> currencyCodes = (from k in RatesList select k.Key).ToList();
            var Currencies = _context.Currencies.ToList();
            foreach (var currencyCode in currencyCodes)
            {
                CurrencyCodeIds.Add(currencyCode, Currencies.SingleOrDefault(c => c.Name == currencyCode).CurrencyId);
            }


            var ExchangeRates = await _context.ExchangeRates.ToListAsync();
           
            foreach (var item in RatesList)
            {
                string baseCurrency = item.Key;
                var baseCurrencyRate = item.Value;
                long baseCurrencyId = CurrencyCodeIds[baseCurrency];
                var ratesListTemp = new List<KeyValuePair<string, double>>
                    (RatesList.Select
                    (r => new KeyValuePair<string, double>(r.Key, r.Value / baseCurrencyRate)));
                foreach (var rate in ratesListTemp)
                {
                    if (rate.Key != baseCurrency)
                    {
                        var SecondCurrencyId = CurrencyCodeIds[rate.Key];
                        if (ExchangeRates.Exists(r => r.FirstCurrencyId == baseCurrencyId && r.SecondCurrencyId == SecondCurrencyId))
                        {
                            var rateToUpdate = await _context.ExchangeRates.SingleOrDefaultAsync(r => r.FirstCurrencyId == baseCurrencyId && r.SecondCurrencyId == SecondCurrencyId);
                            rateToUpdate.Rate = rate.Value;
                        }

                        var exRate = new ExchangeRate();
                        exRate.FirstCurrencyId = baseCurrencyId;
                        exRate.SecondCurrencyId = SecondCurrencyId;
                        exRate.Rate = rate.Value;
                        await _context.ExchangeRates.AddAsync(exRate);

                    }
                }

                await _context.SaveChangesAsync();





            }

            
            


        }



        //public void 
    }
}
