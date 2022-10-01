using Discord;

namespace AmaiClan.Bot.Services
{
    public class ConsoleLogger : ILogger
    {
        public Task Log(string arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public Task Log(LogMessage arg)
        {
            Console.WriteLine(arg.Message);
            return Task.CompletedTask;
        }
    }
}