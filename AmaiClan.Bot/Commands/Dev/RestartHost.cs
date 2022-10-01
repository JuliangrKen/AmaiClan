using AmaiClan.Bot.Configuration;
using Discord.Interactions;
using System.Diagnostics;

namespace AmaiClan.Bot.Commands.Dev
{
    public class RestartHost : SlashCommandBase
    {
        private readonly DiscordBotConfig discordBotConfig;

        public RestartHost(DiscordBotConfig discordBotConfig)
        {
            this.discordBotConfig = discordBotConfig;
        }

        [SlashCommand("restart-host", "[dev] перезапустить хост")]
        public async Task Invoke()
        {
            if (discordBotConfig.DevDiscordID != Context.User.Id)
                return;

            await RespondAsync("Начинаем перезапуск хоста!");

            Process.Start("shutdown", "/r /t 0");
        }
    }
}