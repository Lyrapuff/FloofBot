using System;
using System.Net.Http;
using System.Threading.Tasks;
using Derpibooru.Models;
using FloofBot.Core.Common;
using FloofBot.Core.Services;
using Newtonsoft.Json;

namespace Derpibooru.Services
{
    public class DerpibooruService : IFloofyService
    {
        private Logger _logger;
        private readonly HttpClient _httpClient;

        public DerpibooruService(ILoggerProvider loggerProvider)
        {
            _logger = loggerProvider.GetLogger("Derpibooru");
            _httpClient = new HttpClient();
        }
        
        public async Task<DerpiImage[]> Search(string query)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://derpibooru.org/api/v1/json/search/images?q={query}");
            string json = await response.Content.ReadAsStringAsync();

            DerpiImage[] images = null;

            try
            {
                images = JsonConvert.DeserializeObject<DerpiImage[]>(json);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in Derpibooru image search:{Environment.NewLine}{e}");
            }

            return images;
        }
    }
}