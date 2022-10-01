using AmaiClan.Bot.Configuration;
using Discord.Interactions;
using System.Diagnostics;
using System.Reflection;

namespace AmaiClan.Bot.Commands.Dev
{
    public class RestartBot : SlashCommandBase
    {
        private readonly DiscordBotConfig discordBotConfig;

        public RestartBot(DiscordBotConfig discordBotConfig)
        {
            this.discordBotConfig = discordBotConfig;
        }

        [SlashCommand("restart-bot", "[dev] перезапустить бота")]
        public async Task Invoke()
        {
            if (discordBotConfig.DevDiscordID != Context.User.Id)
                return;

            var exeFileApp = Assembly.GetExecutingAssembly().Location[..^3] + "exe";

            if (!File.Exists(exeFileApp))
            {
                await RespondAsync("Не удалось перезапустить бота!");
                return;
            }

            await RespondAsync("Начинаем перезапуск бота!");

            Process.Start(exeFileApp);
            Environment.Exit(0);
        }
    }
}
