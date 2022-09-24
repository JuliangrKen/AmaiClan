using Discord;
using Discord.Interactions;

namespace AmaiClan.Bot.Commands.Everyone
{
    public class Avatar : SlashCommandBase
    {
        [SlashCommand("avatar", "получить случайное число")]
        public async Task Invoke(IUser user)
        {
            var avatarUrl = user.GetAvatarUrl(ImageFormat.Auto, 2048);

            var embedBuilder = new EmbedBuilder()
                .WithTitle(user.Username)
                .WithUrl(avatarUrl)
                .WithImageUrl(avatarUrl)
                .WithColor(GetRandomColor());

            await RespondAsync(embed: embedBuilder.Build());
        }
    } 
}