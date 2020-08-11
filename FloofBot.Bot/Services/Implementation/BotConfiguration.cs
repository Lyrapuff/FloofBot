using System.IO;
using Newtonsoft.Json;

namespace FloofBot.Bot.Services.Implementation
{
    public class BotConfiguration : IBotConfiguration
    {
        public BotCredentials Credentials { get; set; }

        public BotConfiguration()
        {
            string path = "Config.json";
            
            if (!File.Exists(path))
            {
                File.Create(path);
                File.WriteAllText(path, JsonConvert.SerializeObject(new BotConfiguration()));
            }

            string json = File.ReadAllText(path);
            JsonConvert.PopulateObject(json, this);
        }
    }
}