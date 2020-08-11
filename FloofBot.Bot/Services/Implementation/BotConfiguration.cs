using System;
using System.IO;
using Newtonsoft.Json;

namespace FloofBot.Bot.Services.Implementation
{
    public class BotConfiguration : IBotConfiguration
    {
        [JsonProperty]
        public BotCredentials Credentials { get; set; } = new BotCredentials();

        public BotConfiguration()
        {
            string path = "Config.json";
            
            if (!File.Exists(path))
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
                }

                return;
            }

            string json = File.ReadAllText(path);
            JsonConvert.PopulateObject(json, this);
        }
    }
}