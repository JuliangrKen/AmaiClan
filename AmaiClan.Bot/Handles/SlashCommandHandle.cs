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

        public SlashCommandHandle(IServiceProvider serviceProvider, DiscordSocketClient discordSocketClient)
        {
            this.serviceProvider = serviceProvider;
            interactionService = new InteractionService(discordSocketClient);
            this.discordSocketClient = discordSocketClient;
        }

        public async Task HandleAsync()
        {
            await interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);
            await RegisterCommandForEveryoneGuilds(discordSocketClient);

            discordSocketClient.InteractionCreated += async interaction =>
            {
                var scope = serviceProvider.CreateScope();
                var ctx = new SocketInteractionContext(discordSocketClient, interaction);
                await interactionService.ExecuteCommandAsync(ctx, scope.ServiceProvider);
            };
        }

        private Task RegisterCommandForEveryoneGuilds(DiscordSocketClient client)
        {
            foreach(var guild in client.Guilds)
                interactionService.RegisterCommandsToGuildAsync(guild.Id);

            return Task.CompletedTask;
        }
    }
}