using System;

namespace FloofBot.Bot.Common
{
    public class Logger
    {
        private string _name;
        
        public Logger(string name)
        {
            _name = name;
        }

        private void Log(object data, string logType, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"[{logType}][{DateTime.Now:t}][{_name}] {data}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        
        public void LogInformation(object data)
        {
            Log(data, "Info", ConsoleColor.Gray);
        }

        public void LogWarning(object data)
        {
            Log(data, "Warn", ConsoleColor.DarkYellow);
        }
        
        public void LogError(object data)
        {
            Log(data, "Fail", ConsoleColor.DarkRed);
        }
        
        public void LogDebug(object data)
        {
            Log(data, "Dbug", ConsoleColor.Cyan);
        }
    }
}