using AmaiClan.Bot.Configuration;
using AmaiClan.Bot.Handles;
using AmaiClan.Bot.Services;
using Discord;
using Discord.WebSocket;

namespace AmaiClan.Bot
{
    /// <summary>
    /// Основной класс для работы с ботом, во многом являющийся фасадом DiscordSocketClient
    /// </summary>
    public class DiscordBot
    {
        public DiscordSocketClient SocketClient { get; }

        private readonly DiscordBotConfig botConfig;
        
        // Services:
        private readonly ILogger logger;
        private readonly SlashCommandHandle slashCommandHandle;

        public DiscordBot(DiscordSocketClient socketClient,
            DiscordBotConfig botConfig, 
            ILogger logger,
            SlashCommandHandle slashCommandHandle)
        {
            SocketClient = socketClient;

            this.botConfig = botConfig;
            this.logger = logger;
            this.slashCommandHandle = slashCommandHandle;
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
            // Все сервисы, добавленные в Program, будут подключены ниже
            SocketClient.Log += (LogMessage message) => logger.Log(message);

            // Используем обработчики
            SocketClient.Ready += () => slashCommandHandle.HandleAsync();
            SocketClient.UserJoined += (SocketGuildUser user) => user.AddRoleAsync(botConfig.AutoRoleID);
        }
    }
}