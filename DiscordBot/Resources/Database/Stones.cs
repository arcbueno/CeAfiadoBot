using System.ComponentModel.DataAnnotations;

namespace DiscordBot.Resources.Database
{
    public class Stones
    {
        [Key]
        public ulong UserId { get; set; }
        public ulong Amount { get; set; }
    }
}