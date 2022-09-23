using Discord;

namespace AmaiClan.Bot.Services
{
    public interface ILogger
    {
        Task Log(LogMessage arg);
    }
}