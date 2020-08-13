using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FloofBot.Core.Common;
using Newtonsoft.Json;

namespace FloofBot.Core.Services.Implementation
{
    public class Localization : ILocalization
    {
        private Logger _logger;
        private List<LocalizationKey> _keys = new List<LocalizationKey>();
        private List<Locale> _locales = new List<Locale>();

        public Localization(ILoggerProvider loggerProvider)
        {
            _logger = loggerProvider.GetLogger("Main");

            try
            {
                Stopwatch timer = Stopwatch.StartNew();
                int count = 0;

                string path = "Locales";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                foreach (FileInfo fileInfo in directoryInfo.GetFiles("keys.json"))
                {
                    string text = File.ReadAllText(fileInfo.FullName);
                    _keys = JsonConvert.DeserializeObject<List<LocalizationKey>>(text);
                }

                foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.locale.json"))
                {
                    string text = File.ReadAllText(fileInfo.FullName);
                    string key = fileInfo.Name.Split('.')[0];

                    Locale locale = new Locale();
                    locale.Key = key;
                    locale.Words = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
                    
                    _locales.Add(locale);

                    count++;
                }

                timer.Stop();

                _logger.LogInformation($"Loaded {count} {(count == 1 ? "locale" : "locales")} in {timer.Elapsed:g}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in Localization:{Environment.NewLine}{e}");
            }
        }
        
        public string GetString(string localeKey, string wordKey)
        {
            string defaultValue = "No localization 3:";

            try
            {
                Locale locale = _locales.FirstOrDefault(x => x.Key == localeKey);
                
                if (locale != null)
                {
                    string word = locale.Words.FirstOrDefault(x => x.Key == wordKey).Value;

                    return string.IsNullOrWhiteSpace(word) ? defaultValue : word;
                }

                return defaultValue;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting localization value:{Environment.NewLine}{e}");   
                return defaultValue;
            }
        }
    }

    public class LocalizationKey
    {
        public string Key { get; set; }
        public bool Overridable { get; set; }
    }

    public class Locale
    {
        [JsonIgnore]
        public string Key { get; set; }
        public Dictionary<string, string> Words { get; set; }
    }
}