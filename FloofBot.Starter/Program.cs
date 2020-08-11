﻿using System.Threading.Tasks;
using FloofBot.Bot;

namespace FloofBot.Starter
{
    internal static class Program
    {
        public static async Task Main()
        {
            await new BotSetup().Setup();
            await Task.Delay(-1);
        }
    }
}