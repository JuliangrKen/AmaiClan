using AmaiClan.Bot.Configuration;
using AmaiClan.Bot.Handles;
using AmaiClan.Bot.Services;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace AmaiClan.Bot
{
    /// <summary>
    /// Основной класс для создания бота, во многом являющийся фасадом DiscordSocketClient
    /// </summary>
    public class DiscordBot
    {
        public static IServiceCollection Services { get; } = new ServiceCollection();

        public DiscordSocketClient SocketClient { get; }


        private readonly DiscordBotConfig botConfig;
        
        public DiscordBot(DiscordSocketConfig socketConfig, DiscordBotConfig botConfig)
        {
            Services.AddSingleton(this);
            SocketClient = new DiscordSocketClient(socketConfig);
            this.botConfig = botConfig;
        }

        /// <summary>
        /// Метод запуска бота
        /// </summary>
        public async Task Run()
        {
            BuildServices();

            await SocketClient.LoginAsync(TokenType.Bot, botConfig.Token);
            await SocketClient.StartAsync();
            await Task.Delay(-1);
        }

        private void BuildServices()
        {
            var build = Services.BuildServiceProvider();

            // Все сервисы, добавленные в Program, будут подключены ниже
            SocketClient.Log += (LogMessage message) => build.GetService<ILogger>()?.Log(message);

            // Используем обработчики
            SocketClient.Ready += () => build.GetService<SlashCommandHandle>()?.HandleAsync();
        }
    }
}