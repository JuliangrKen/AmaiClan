using AmaiClan.Bot.Configuration;
using Discord.Interactions;

namespace AmaiClan.Bot.Commands.Dev
{
    public class HostInfo : SlashCommandBase
    {
        private readonly DiscordBotConfig discordBotConfig;

        public HostInfo(DiscordBotConfig discordBotConfig)
        {
            this.discordBotConfig = discordBotConfig;
        }

        [SlashCommand("host-info", "[dev] получить информацию о хосте")]
        public async Task Invoke(bool ephemeral = true)
        {
            if (discordBotConfig.DevDiscordID != Context.User.Id)
                return;

            var lifeTime = new DateTime(Environment.TickCount);

            var textInfo = $"OC: {Environment.OSVersion}\n" +
                $"MachineName: {Environment.MachineName}\n" +
                $"ProcessId: {Environment.ProcessId}\n" +
                $"WorkingSet: {Environment.WorkingSet} MB\n" +
                $"CLR: {Environment.Version}\n" +
                $"SystemPageSize: {Environment.SystemPageSize}\n" +
                $"\n" +
                $"Запущен: {DateTime.Now - lifeTime}\n" +
                $"Работает уже: {lifeTime}\n" +
                $"Запущен";

            await RespondAsync($"```cs\n{textInfo}\n```", ephemeral: ephemeral);
        }
    }
}