using Discord;

namespace AmaiClan.Bot.Services
{
    public interface ILogger
    {
        Task Log(string arg);
        Task Log(LogMessage arg);
    }
}