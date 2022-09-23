using AmaiClan.Bot;
using AmaiClan.Bot.Configuration;
using AmaiClan.Bot.Handles;
using AmaiClan.Bot.Services;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

// Конфигурация бота
var socketConfig = new DiscordSocketConfig()
{
    AlwaysDownloadUsers = true,
    ConnectionTimeout = 10_000,
    MessageCacheSize = 100,
    GatewayIntents = GatewayIntents.None
};

// Инициализация, настойка и запуск бота
var discordBot = new DiscordBot(socketConfig, DiscordBotConfig.GetFromFile());


// Добавляем сервисы
DiscordBot.Services.AddSingleton<ILogger, ConsoleLogger>();
DiscordBot.Services.AddTransient<SlashCommandHandle>();

await discordBot.Run();

