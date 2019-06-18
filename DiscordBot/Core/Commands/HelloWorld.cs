using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Discord;
using Discord.Commands;


namespace DiscordBot.Core.Commands
{
    public class HelloWorld : ModuleBase<SocketCommandContext>
    {
        [Command("hello"), Alias("helloworld", "world"), Summary("Hello world command")]
        public async Task Bot()
        {
            await Context.Channel.SendMessageAsync("Hello world");
        }

        [Command("say"), Alias("fale", "diga")]
        public async Task Say([Remainder] string Input = "None")
        {
            if (Input == "None")
            {
                await Context.Channel.SendMessageAsync("Falar o que? Cacete");
            }
            else
            {
                await Context.Channel.SendMessageAsync(Input);
            }
        }

        [Command("embed"), Summary("Embbed test Command")]
        public async Task Embed([Remainder] string Input = "None")
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor(Context.User.Username, Context.User.GetAvatarUrl());
            Embed.WithFooter(DateTime.Now.ToString(), Context.Guild.Owner.GetAvatarUrl());
            Embed.WithDescription("[Wow, this is a **link** muthafocka](https://www.google.com/)");
            Embed.AddField("User Input:", Input);
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}