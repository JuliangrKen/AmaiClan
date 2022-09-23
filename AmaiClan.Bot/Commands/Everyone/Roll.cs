using Discord;
using Discord.Interactions;

namespace AmaiClan.Bot.Commands.Everyone
{
    /// <summary>
    /// Команда получения случайного числа
    /// </summary>
    public class Roll : SlashCommandBase
    {
        [SlashCommand("roll", "получить случайное число")]
        public async Task Invoke(int max = 100, int min = 1)
        {
            (max, min) = max < min ? (min, max) : (max, min);

            var result = new Random().Next(min, max);

            var embedBuilder = new EmbedBuilder()
                .WithTitle($"Результат: {result}")
                .WithFooter($"Случайное число от {min} до {max}.")
                .WithColor(GetRandomColor());

            await RespondAsync(embed: embedBuilder.Build());
        }
    }
}