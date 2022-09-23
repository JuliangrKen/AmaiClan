using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AmaiClan.Bot.Handles
{
    public class SlashCommandHandle
    {
        private readonly DiscordBot discordBot;
        private readonly InteractionService interactionService;

        public SlashCommandHandle(DiscordBot discordBot)
        {
            this.discordBot = discordBot;
            interactionService = new InteractionService(discordBot.SocketClient);
        }

        public async Task HandleAsync()
        {
            var provider = DiscordBot.Services.BuildServiceProvider();
            var client = discordBot.SocketClient;

            await interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), provider);
            await RegisterCommandForEveryoneGuilds(client);

            client.InteractionCreated += async interaction =>
            {
                var scope = provider.CreateScope();
                var ctx = new SocketInteractionContext(client, interaction);
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