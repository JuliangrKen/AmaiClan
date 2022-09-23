﻿using AmaiClan.Bot;
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

var services = new ServiceCollection();

// Добавляем сервисы:
services.AddSingleton(new DiscordSocketClient(socketConfig));
services.AddSingleton(DiscordBotConfig.GetFromFile());
services.AddSingleton<ILogger, ConsoleLogger>();
// Обработчики:
services.AddTransient<SlashCommandHandle>();

services.AddSingleton<DiscordBot>();

var discordBot = services.BuildServiceProvider().GetService<DiscordBot>();
if (discordBot == null)
    throw new ArgumentNullException();

await discordBot.Run();