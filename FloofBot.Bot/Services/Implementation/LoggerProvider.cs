﻿using System.Collections.Generic;
using FloofBot.Bot.Common;

namespace FloofBot.Bot.Services.Implementation
{
    public class LoggerProvider : ILoggerProvider
    {
        private Dictionary<string, Logger> _loggers = new Dictionary<string, Logger>();
        
        public Logger GetLogger(string name)
        {
            Logger logger;
            
            if (_loggers.ContainsKey(name))
            {
                logger = _loggers[name];
            }
            else
            {
                logger = new Logger(name);
                _loggers[name] = logger;
            }
            
            return logger;
        }
    }
}