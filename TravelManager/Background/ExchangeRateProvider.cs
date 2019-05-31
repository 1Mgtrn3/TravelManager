using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TravelManager.Models;

namespace TravelManager.Background
{
    public class ExchangeRateProvider
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
            Start();
        }


        public void Start()
        {
           var ExRates = GetAsync<ExchangeRatesResponeRoot>(BaseEndpoint);

        }

        private async Task<T> GetAsync<T>(Uri requestUrl)
        {

            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }



        //public void 
    }
}
