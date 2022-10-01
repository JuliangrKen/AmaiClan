using AmaiClan.Bot.Services;
using Discord;
using Discord.Interactions;

namespace AmaiClan.Bot.Commands.Everyone
{
    public class RandomAnime : SlashCommandBase
    {
        private readonly IAnimeGoParser animeGoParser;

        public RandomAnime(IAnimeGoParser animeGoParser)
        {
            this.animeGoParser = animeGoParser;
        }

        [SlashCommand("random-anime", "получить случайное аниме")]
        public async Task Invoke()
        {
            try
            {
                var animeModel = await animeGoParser.GetRandomAnimeGoAnimeInfoAsync();

                var embedBuilder = new EmbedBuilder()
                    .WithTitle(animeModel.Name)
                    .WithUrl(animeModel.SiteUrl)
                    .WithImageUrl(animeModel.ImageUrl)
                    .WithColor(GetRandomColor());

                await RespondAsync(embed: embedBuilder.Build());
            }
            catch
            {
                await RespondAsync("Произошла ошибка, повторите попытку!", ephemeral: true);
            }
        }
    }
}