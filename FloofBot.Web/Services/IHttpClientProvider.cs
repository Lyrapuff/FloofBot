using System.Net.Http;

namespace FloofBot.Web.Services
{
    public interface IHttpClientProvider
    {
        HttpClient GetClient();
    }
}