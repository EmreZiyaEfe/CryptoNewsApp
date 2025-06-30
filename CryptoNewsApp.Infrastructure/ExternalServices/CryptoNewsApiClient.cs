using CryptoNewsApp.Application.Dtos;
using CryptoNewsApp.Application.Interfaces;
using CryptoNewsApp.Infrastructure.ExternalServices.CryptoPanic.Models;
using CryptoNewsApp.Infrastructure.ExternalServices.NewsData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNewsApp.Infrastructure.ExternalServices
{
    public class CryptoNewsApiClient : INewsApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CryptoNewsApiClient> _logger;
        private readonly IConfiguration _configuration;

        public CryptoNewsApiClient(HttpClient httpClient, ILogger<CryptoNewsApiClient> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IEnumerable<NewsDto>> GetLatestNewsAsync(int count)
        {
            try
            {
                var apiUrl = _configuration["CryptoNewsApi:BaseUrl"];
                var apiKey = _configuration["CryptoNewsApi:ApiKey"];
                //var fullUrl = $"{apiUrl}?apikey={apiKey}&q=crypto&language=en";

                var fullUrl = $"{apiUrl}?auth_token={apiKey}&public=true&limit={count}";

                Console.WriteLine("API URL: " + fullUrl);


                var response = await _httpClient.GetFromJsonAsync<CryptoPanicResponse>(fullUrl);

                if (response?.Results == null)
                    return new List<NewsDto>();

                return response.Results
                               .Take(count)
                               .Select(p => new NewsDto
                               {
                                   //Title = p.Title,
                                   //Content = p.Content,
                                   //Description = p.Description,
                                   //Url = p.Link,
                                   //PublishDate = DateTime.TryParse(p.PubDate, out var parsedDate) ? parsedDate : DateTime.MinValue,
                                   //ImageUrl = p.Image_Url ?? "https://placehold.co/300x150",
                                   //Source = new SourceInfo
                                   //{
                                   //    Title = p.SourceId ?? "Unknown",
                                   //    Domain = p.SourceId ?? "Unknown",
                                   //    Region = "en",
                                   //    Path = null,
                                   //    Type = "feed"
                                   //}


                                   //cryptopanic api

                                   Title = p.Title,
                                   Url = p.Url,
                                   PublishDate = p.PublishedAt,
                                   Source =new SourceInfo
                                   {
                                       Title = p.Source?.Title,
                                       Region = p.Source?.Region,
                                       Domain = p.Source?.Domain,
                                       Path = p.Source?.Path,
                                       Type = p.Source?.Type
                                   },
                                   ImageUrl = p.Thumbnail,
                                   Content = null // CryptoPanic'te içerik (content) genelde yoktur
                               });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Haberler apidan alınırken hata oluştu");
                return new List<NewsDto>();
            }
        }
    }
}
