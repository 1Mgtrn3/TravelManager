using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly ILogger _logger;

        private Uri BaseEndpoint { get; set; }
        public ExchangeRateProvider(TravelManagerContext context, IConfiguration configuration, ILogger<ExchangeRateProvider> logger)
        {
            _context = context;
            _configuration = configuration;
            BaseEndpoint = new Uri(_configuration.GetValue<string>("ExchangeRatesAPIurl"), UriKind.Absolute);

            _httpClient = new HttpClient();
            _logger = logger;
            _logger.LogInformation($"LOG: ExchangeRateProvider created");
            _logger.LogInformation($"LOG: BaseEndpoint: {BaseEndpoint}");

        }


        public async Task Start()
        {
            _logger.LogInformation($"LOG: Start - started");
            var ExRatesRAW = await GetDataAsync(BaseEndpoint);
           var RatesList = ExtractRates(ExRatesRAW);
           await PushToDB(RatesList);
        }
        public List<KeyValuePair<string, double>> ExtractRates(string ExRatesRAW) {
            _logger.LogInformation($"LOG: ExtractRates - started");
            _logger.LogInformation($"LOG: ExRatesRAW : {ExRatesRAW}");
            var resource = JObject.Parse(ExRatesRAW);
            string ratesNode = "rates";
            List<KeyValuePair<string, double>> RatesList = new List<KeyValuePair<string, double>>();
            _logger.LogInformation($"LOG: resource.Properties().Count : {resource.Properties().AsEnumerable().Count()}");
            foreach (var property in resource.Properties())
            {

                _logger.LogInformation($"LOG: Currently working on {property.Name} ");
                if (property.Name == ratesNode)
                {
                    _logger.LogInformation($"LOG: Entering rates node");
                    foreach (var item in property.Value.AsJEnumerable())
                    {

                        //Console.WriteLine("Item: " + item);
                        var itemPath = item.Path;
                        _logger.LogInformation($"LOG: itemPath: {itemPath}");
                        //Console.WriteLine($"item path: " + itemPath);
                        string itemName = itemPath.Substring(ratesNode.Length + 1);
                        _logger.LogInformation($"LOG: itemName: {itemName}");
                        string itemValue = item.ToString().Substring(itemName.Length + 4);
                        _logger.LogInformation($"LOG: itemValue: {itemValue}");
                        double resultValue;

                        Double.TryParse(itemValue, NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), out resultValue);
                        _logger.LogInformation($"LOG: resultValue: {resultValue}");
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
            _logger.LogInformation($"LOG: GetDataAsync - started");

            var response = await _httpClient.GetAsync(BaseEndpoint, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            
            return data;
        }

        private async Task PushToDB(List<KeyValuePair<string, double>> RatesList) {

            _logger.LogInformation($"LOG: PushToDB - started");
            _logger.LogInformation($"LOG: RatesList.Count: {RatesList.Count}");


            Dictionary<string, long> CurrencyCodeIds = new Dictionary<string, long>();

            List<string> currencyCodes = (from k in RatesList select k.Key).ToList();
            _logger.LogInformation($"LOG: currencyCodes.Count: {currencyCodes.Count}");

            var Currencies = await _context.Currencies.ToListAsync();
            _logger.LogInformation($"LOG: Currencies.Count: {Currencies.Count}");

            foreach (var currencyCode in currencyCodes)
            {
                CurrencyCodeIds.Add(currencyCode, Currencies.SingleOrDefault(c => c.Name == currencyCode).CurrencyId);
            }
            _logger.LogInformation($"LOG: CurrencyCodeIds.Count: {CurrencyCodeIds.Count}");

            var ExchangeRates = await _context.ExchangeRates.ToListAsync();
            _logger.LogInformation($"LOG: ExchangeRates.Count: {ExchangeRates.Count}");
            foreach (var item in RatesList)
            {
                string baseCurrency = item.Key;
                var baseCurrencyRate = item.Value;
                long baseCurrencyId = CurrencyCodeIds[baseCurrency];

                _logger.LogInformation($"LOG: baseCurrency: {baseCurrency} baseCurrencyRate: {baseCurrencyRate} baseCurrencyId: {baseCurrencyId}");
                var ratesListTemp = new List<KeyValuePair<string, double>>
                    (RatesList.Select
                    (r => new KeyValuePair<string, double>(r.Key, r.Value / baseCurrencyRate)));

                _logger.LogInformation($"LOG: ratesListTemp.Count: {ratesListTemp.Count}");
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
                _logger.LogInformation($"LOG: context is about to be saved");
                await _context.SaveChangesAsync();





            }

            
            


        }



        //public void 
    }
}
