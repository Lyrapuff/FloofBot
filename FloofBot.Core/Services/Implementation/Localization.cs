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
        private readonly Dictionary<string, LocalizedWord[]> _locales = new Dictionary<string, LocalizedWord[]>();

        public Localization(ILoggerProvider loggerProvider)
        {
            Logger logger = loggerProvider.GetLogger("Main");

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

                foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.locale.json"))
                {
                    string text = File.ReadAllText(fileInfo.FullName);
                    string localeName = fileInfo.Name.Split('.')[0];

                    _locales[localeName] = JsonConvert.DeserializeObject<LocalizedWord[]>(text);

                    count++;
                }

                timer.Stop();

                logger.LogInformation($"Loaded {count} {(count == 1 ? "locale" : "locales")} in {timer.Elapsed:g}");
            }
            catch (Exception e)
            {
                logger.LogError($"Error in Localization:{Environment.NewLine}{e}");
            }
        }
        
        public string GetString(string locale, string key)
        {
            if (_locales.ContainsKey(locale))
            {
                return _locales[locale].FirstOrDefault(x => x.Key == key).Value ?? "No localization 3:";
            }

            return "No localization 3:";
        }
    }

    public struct LocalizedWord
    {
        public string Key { get; }
        public string Value { get; }

        public LocalizedWord(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}