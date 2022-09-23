using AmaiClan.Bot.Configuration;
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
        public DiscordSocketClient SocketClient { get; }
        public IServiceCollection Services { get; }

        private readonly DiscordBotConfig botConfig;
        
        public DiscordBot(DiscordSocketConfig socketConfig, DiscordBotConfig botConfig)
        {
            SocketClient = new DiscordSocketClient(socketConfig);
            Services = new ServiceCollection().AddSingleton(this);
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
        }
    }
}