﻿using AmaiClan.Bot.Configuration;
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

        [SlashCommand("verify", "верефицировать и добавить роли пользователю")]
        public async Task Invoke(SocketGuildUser user, 
            SocketRole? role1 = null, 
            SocketRole? role2 = null, 
            SocketRole? role3 = null)
        {
            await user.AddRoleAsync(botConfig.VerifiedRoleID);
            await user.RemoveRoleAsync(botConfig.AutoRoleID);

            if (role1 != null)
                await user.AddRoleAsync(role1);

            if (role2 != null)
                await user.AddRoleAsync(role2);

            if (role3 != null)
                await user.AddRoleAsync(role3);

            await RespondAsync($"{user.Nickname} прошёл верификацию.", ephemeral: true);
        }
    }
}