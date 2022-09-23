using AmaiClan.Bot;
using AmaiClan.Bot.Configuration;
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
    GatewayIntents = GatewayIntents.AllUnprivileged
};

// Инициализация, настойка и запуск бота
var discordBot = new DiscordBot(socketConfig, DiscordBotConfig.GetFromFile());

// Добавляем сервисы
discordBot.Services.AddSingleton<ILogger, ConsoleLogger>();

await discordBot.Run();