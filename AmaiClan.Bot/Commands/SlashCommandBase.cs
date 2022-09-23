using Discord;
using Discord.Interactions;
using Microsoft.Extensions.DependencyInjection;

namespace AmaiClan.Bot.Commands
{
    public abstract class SlashCommandBase : InteractionModuleBase<SocketInteractionContext>
    {
        protected DiscordBot DiscordBot { get; set; }

        protected SlashCommandBase()
        {
            DiscordBot = DiscordBot.Services.BuildServiceProvider().GetService<DiscordBot>() ?? 
                throw new ArgumentNullException();
        }

        /// <summary>
        /// Метод для получения случайного цвета
        /// </summary>
        /// <returns></returns>
        protected Color GetRandomColor()
        {
            // Поставлен такой минимум, чтобы цвета не были тёмными
            var min = 150;
            var max = 255;

            var random = new Random();
            return new Color(random.Next(min, max), random.Next(min, max), random.Next(min, max));
        }
    }
}