using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FloofBot.Core.Services.Implementation
{
    public class BotConfiguration : IBotConfiguration
    {
        // should be Development or Release
        public string LogLevel { get; set; } = "Development";
        public string DatabaseConnectionString { get; set; } = "Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=password";
        public BotCredentials Credentials { get; set; } = new BotCredentials();
        public Dictionary<string, Command> Commands { get; set; } = new Dictionary<string, Command>();

        public static BotConfiguration Instance;
        
        public BotConfiguration()
        {
            Instance = this;
            
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
            return Instance.Commands[memberName];
        }
    }
}