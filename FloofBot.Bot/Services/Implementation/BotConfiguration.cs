using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FloofBot.Bot.Services.Implementation
{
    public class BotConfiguration : IBotConfiguration
    {
        // should be Development or Release
        public string LogLevel { get; set; } = "Development";
        public BotCredentials Credentials { get; set; } = new BotCredentials();
        public Dictionary<string, Command> Commands { get; set; } = new Dictionary<string, Command>();

        private static BotConfiguration _instance;
        
        public BotConfiguration()
        {
            _instance = this;
            
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

        public static Command GetCommand(string memberName)
        {
            return _instance.Commands[memberName];
        }
    }
}