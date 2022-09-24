using AmaiClan.Bot.Exceptions;
using System.Text.Json;

namespace AmaiClan.Bot.Configuration
{
    /// <summary>
    /// Класс, инкапсулирующий значения из DiscordBotConfig.json
    /// </summary>
    public class DiscordBotConfig
    {
        public string? Token { get; set; }
        public ulong DevDiscordID { get; set; }
        public ulong GuildID { get; set; }
        public ulong AutoRoleID { get; set; }
        public ulong VerifiedRoleID { get; set; }

        public static DiscordBotConfig GetFromFile(string? path = null)
        {
            path ??= $@"{Environment.CurrentDirectory}\Configuration\DiscordBotConfig.json";
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<DiscordBotConfig>(json) ?? throw new FailedDeserializeObjectException();
        }
    }
}