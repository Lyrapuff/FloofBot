using System.Net.Http;

namespace FloofBot.Web.Services.Implementation
{
    public class HttpClientProvider : IHttpClientProvider
    {
        private HttpClient _client;
        
        public HttpClient GetClient()
        {
            if (_client == null)
            {
                _client = new HttpClient();
            }

            return _client;
        }
    }
}