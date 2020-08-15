using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FloofBot.Web.Models;
using FloofBot.Web.Services.Responses;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FloofBot.Web.Services.Implementation
{
    public class DiscordApi : IDiscordApi
    {
        private DiscordCredentials _discordCredentials;
        private HttpClient _httpClient;
        private const string _apiEndpoint = "https://discord.com/api/v6";

        public DiscordApi(IOptions<DiscordCredentials> discordCredentials, IHttpClientProvider httpClientProvider)
        {
            _discordCredentials = discordCredentials.Value;
            _httpClient = httpClientProvider.GetClient();
        }
        
        public async Task<TokenExchangeResponse> TokenExchange(string code)
        {
            Dictionary<string, string> form = new Dictionary<string, string>();
            
            form.Add("client_id", _discordCredentials.ClientId);
            form.Add("client_secret", _discordCredentials.ClientSecret);
            form.Add("grant_type", "authorization_code");
            form.Add("code", code);
            form.Add("redirect_uri", _discordCredentials.RedirectUrl);
            form.Add("scope", "identify guilds");
            
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _apiEndpoint + "/oauth2/token");
            request.Content = new FormUrlEncodedContent(form);

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            string text = await response.Content.ReadAsStringAsync();

            TokenExchangeResponse result = null;

            try
            {
                result = JsonConvert.DeserializeObject<TokenExchangeResponse>(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // TODO: logging
            }

            return result;
        }
    }
}