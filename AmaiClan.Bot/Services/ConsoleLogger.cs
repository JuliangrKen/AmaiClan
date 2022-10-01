using Discord;

namespace AmaiClan.Bot.Services
{
    public class ConsoleLogger : ILogger
    {
        public Task Log(string arg)
        {
            Console.WriteLine($"[{DateTime.Now}] - {arg}");
            return Task.CompletedTask;
        }

        public Task Log(LogMessage arg) =>
            Log(arg.Message);
    }
}