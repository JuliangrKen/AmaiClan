using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace AmaiClan.Bot.Commands.Admin
{
    public class Clear : SlashCommandBase
    {
        [RequireUserPermission(ChannelPermission.ManageMessages)]
        [SlashCommand("clear", "очистить сообщения канала")]
        public async Task Invoke(int num)
        {
            if (num == 0 || num > 100 || num < 0)
            {
                await RespondAsync("Некорректный ввод!");
                return;
            }

            await RespondAsync($"Задача принята, {num} сообщений будет удалены!");

            Thread.Sleep(1000);

            num++; // + сообщение, отправленное самим ботом

            var messages = await Context.Channel.GetMessagesAsync(num).FlattenAsync();

            var channel = Context.Channel as SocketTextChannel ?? throw new ArgumentException();
            await channel.DeleteMessagesAsync(messages);
        }
    }
}