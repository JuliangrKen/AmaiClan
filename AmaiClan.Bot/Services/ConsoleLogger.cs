using Discord;

namespace AmaiClan.Bot.Services
{
    public class ConsoleLogger : ILogger
    {
        public Task Log(LogMessage arg)
        {
            Console.WriteLine(arg.Message);
            return Task.CompletedTask;
        }
    }
}