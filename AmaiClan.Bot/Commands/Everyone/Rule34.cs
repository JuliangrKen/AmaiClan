using AmaiClan.Bot.Services;
using Discord;
using Discord.Interactions;

namespace AmaiClan.Bot.Commands.Everyone
{
    public class Rule34 : SlashCommandBase
    {
        private readonly IRule34Parser rule34Parser;

        public Rule34(IRule34Parser rule34Parser)
        {
            this.rule34Parser = rule34Parser;
        }

        [RequireNsfw]
        [SlashCommand("rule34", "получить случайный порно-арт (только в nsfw-каналах)")]
        public async Task Invoke()
        {
            try
            {
                var url = await rule34Parser.GetRandomImageUrlAsync();

                var embedBuilder = new EmbedBuilder()
                    .WithImageUrl(url)
                    .WithColor(GetRandomColor());

                await RespondAsync(embed: embedBuilder.Build(), ephemeral: true);
            }
            catch
            {
                await RespondAsync("Произошла ошибка, повторите попытку!");
            }
        }
    }
}