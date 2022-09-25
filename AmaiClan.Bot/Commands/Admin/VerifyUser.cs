using AmaiClan.Bot.Configuration;
using Discord.Interactions;
using Discord.WebSocket;

namespace AmaiClan.Bot.Commands.Admin
{
    public class VerifyUser : SlashCommandBase
    {
        private readonly DiscordBotConfig botConfig;

        public VerifyUser(DiscordBotConfig botConfig)
        {
            this.botConfig = botConfig;
        }

        [SlashCommand("verify", "добавить роль пользователю")]
        public async Task Invoke(SocketGuildUser user)
        {
            await user.AddRoleAsync(botConfig.VerifiedRoleID);
            await user.RemoveRoleAsync(botConfig.AutoRoleID);

            await RespondAsync($"{user.Nickname} прошёл верификацию.", ephemeral: true);
        }
    }
}