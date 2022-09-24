using AmaiClan.Bot.Configuration;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AmaiClan.Bot.Handles
{
    public class SlashCommandHandle
    {
        private readonly IServiceProvider serviceProvider;
        private readonly InteractionService interactionService;
        private readonly DiscordSocketClient discordSocketClient;
        private readonly DiscordBotConfig discordBotConfig;

        public SlashCommandHandle(IServiceProvider serviceProvider, 
            DiscordSocketClient discordSocketClient, 
            DiscordBotConfig discordBotConfig)
        {
            this.serviceProvider = serviceProvider;
            interactionService = new InteractionService(discordSocketClient);
            this.discordSocketClient = discordSocketClient;
            this.discordBotConfig = discordBotConfig;
        }

        public async Task HandleAsync()
        {
            await interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);
            await interactionService.RegisterCommandsToGuildAsync(discordBotConfig.GuildID);

            discordSocketClient.InteractionCreated += async interaction =>
            {
                var scope = serviceProvider.CreateScope();
                var ctx = new SocketInteractionContext(discordSocketClient, interaction);
                await interactionService.ExecuteCommandAsync(ctx, scope.ServiceProvider);
            };
        }
    }
}