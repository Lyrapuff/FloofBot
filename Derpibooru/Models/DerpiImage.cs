using Newtonsoft.Json;

namespace Derpibooru.Models
{
    public class DerpiImage
    {
        [JsonProperty("view_url")]
        public string ViewUrl { get; set; }
    }
}