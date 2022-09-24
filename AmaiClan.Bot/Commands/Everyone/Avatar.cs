using Discord;
using Discord.Interactions;

namespace AmaiClan.Bot.Commands.Everyone
{
    public class Avatar : SlashCommandBase
    {
        [SlashCommand("avatar", "получить аватар пользователя")]
        public async Task Invoke(IUser user, AvatarSize size = AvatarSize.Middle)
        {
            // Выбор одного из возможных размеров
            ushort urlSize = size switch
            {
                AvatarSize.Min => 128,
                AvatarSize.Middle => 1024,
                AvatarSize.Max => 2048,
                _ => throw new ArgumentException()
            };

            var avatarUrl = user.GetAvatarUrl(ImageFormat.Auto, urlSize) ?? user.GetDefaultAvatarUrl();

            var embedBuilder = new EmbedBuilder()
                .WithTitle(user.Username)
                .WithUrl(avatarUrl)
                .WithImageUrl(avatarUrl)
                .WithColor(GetRandomColor());

            await RespondAsync(embed: embedBuilder.Build());
        }
        
        public enum AvatarSize : byte
        {
            Min,
            Middle,
            Max
        }
    } 
}