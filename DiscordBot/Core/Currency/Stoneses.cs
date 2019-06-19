using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Core.Data; 
namespace DiscordBot.Core.Currency
{
    public class Stoneses : ModuleBase<ShardedCommandContext>
    {
        [Group("stone"), Alias("stones")]
        public class StonesGroup : ModuleBase<SocketCommandContext>
        {
            [Command("me"), Alias("me","eu", "my")]
            public async Task Me()
            {
                await Context.Channel.SendMessageAsync(
                    $"{Context.User}, you have {Data.Data.GetStones(Context.User.Id)} stones!");
                return;
            }

            [Command("give"), Alias("gift")]
            public async Task Give(IUser user = null, ulong Amount = 0)
            {
                // Verifications to see if the data inserted is valid
                if (user == null)
                {
                    await Context.Channel.SendMessageAsync(":x: You did not mention a user to give stones");
                    return;
                }
                if (user.IsBot)
                {
                    await Context.Channel.SendMessageAsync(":x: Bots cannot use this bot");
                    return;
                }
                if (Amount <= 0 )
                {
                    await Context.Channel.SendMessageAsync($":x: Please specify a amount of stones to give to {user.Username}");
                    return;
                }
                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if (!User1.GuildPermissions.Administrator)
                {
                    await Context.Channel.SendMessageAsync($":x: You don't have administrator permissions");
                    return;
                }
                
                //Execution 
                await Context.Channel.SendMessageAsync($":tada: {user.Mention} you have received {Amount} stones from {Context.User.Username}");
                
                
                //Save data
                await Data.Data.SaveStones(User1.Id, Amount);

            }
        }
    }
}