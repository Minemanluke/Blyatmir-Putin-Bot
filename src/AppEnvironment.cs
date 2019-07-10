﻿using System;

namespace Blyatmir_Putin_Bot
{
    public class AppEnvironment
    {
        private static string _botToken = "BOT_TOKEN";
        private static string _botPrefix = "BOT_PREFIX";

        public static string BotToken => _botToken;
        public static string BotPrefix => _botPrefix;
        public static string ConfigLocation{ get => "config/"; }

        /// <summary>
        /// Load environment variables
        /// </summary>
        public static void LoadVariables()
        {
            //pull the values from the environment
            string botToken = Environment.GetEnvironmentVariable("BOT_TOKEN");
            string botPrefix = Environment.GetEnvironmentVariable("BOT_PREFIX");

            //if the above values are not null and Bot_Token isn't default value
            //assign value
            if (!string.IsNullOrWhiteSpace(botToken) && BotToken == "BOT_TOKEN")
                _botToken = botToken;

            //if the above values are not null and Bot_Prefix isn't default value
            //assign value
            if (!string.IsNullOrWhiteSpace(botPrefix) && BotPrefix == "BOT_PREFIX")
                _botPrefix = botPrefix;
        }
    }
}
