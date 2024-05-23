using DiscordBot.commands;
using DiscordBot.config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;


namespace DiscordBot
{
    internal class Program
    {
        private static DiscordClient client { get; set; }
        private static CommandsNextExtension commands { get; set; }

        static async Task Main(string[] args)
        {
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            client = new DiscordClient(discordConfig);

            client.Ready += Client_Ready;

            var commandConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { jsonReader.prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
            };

            commands = client.UseCommandsNext(commandConfig);

            commands.RegisterCommands<TestCommands> ();

            await client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
