using AmaiClan.Bot;
using AmaiClan.Bot.Commands.Admin;
using AmaiClan.Bot.Commands.Everyone;
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
    GatewayIntents = GatewayIntents.All
};

var services = new ServiceCollection();

services.AddSingleton<HttpClient>();
// Добавляем сервисы:
services.AddSingleton(new DiscordSocketClient(socketConfig));
services.AddSingleton(DiscordBotConfig.GetFromFile());
services.AddSingleton<ILogger, ConsoleLogger>();
services.AddTransient<IRule34Parser, Rule34Parser>();
// Обработчики:
services.AddTransient<SlashCommandHandle>();

services.AddSingleton<DiscordBot>();

var discordBot = services.BuildServiceProvider().GetService<DiscordBot>();
if (discordBot == null)
    throw new ArgumentNullException();

await discordBot.Run();