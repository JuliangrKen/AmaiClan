using Discord;
using Discord.Interactions;

namespace AmaiClan.Bot.Commands.Admin
{
    /// <summary>
    /// Команда для создания Embed
    /// </summary>
    public class Embed : SlashCommandBase
    {
        [SlashCommand("embed", @"создать embed (\n для переноса строки в description)")]
        public async Task Invoke(
            string? author = null,
            string? title = null,
            string? description = null,
            string? footer = null,
            string? imageUrl = null
            )
        {
            var embedBuilder = new EmbedBuilder();

            embedBuilder.WithAuthor(author);
            embedBuilder.WithTitle(title);
            embedBuilder.WithDescription(description?.Replace(@"\n", "\n"));
            embedBuilder.WithFooter(footer);
            embedBuilder.WithImageUrl(imageUrl);
            embedBuilder.WithColor(GetRandomColor());

            await RespondAsync("Принято в работу", ephemeral: true);
            await Context.Channel.SendMessageAsync(embed: embedBuilder.Build());
        }
    }
}