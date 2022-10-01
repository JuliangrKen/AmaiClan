using Discord;
using Discord.Interactions;

namespace AmaiClan.Bot.Commands
{
    public abstract class SlashCommandBase : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly int minIndexColor;
        private readonly int maxIndexColor;

        protected SlashCommandBase()
        {
            // Поставлен такой минимум, чтобы цвета не были тёмными
            minIndexColor = 150;
            maxIndexColor = 255;
        }

        /// <summary>
        /// Метод для получения случайного цвета
        /// </summary>
        /// <returns></returns>
        protected Color GetRandomColor()
        {
            var random = new Random();
            return new Color(random.Next(minIndexColor, maxIndexColor), 
                random.Next(minIndexColor, maxIndexColor), 
                random.Next(minIndexColor, maxIndexColor));
        }
    }
}