using System.Linq;
using System.Threading.Tasks;
using DiscordBot.Resources.Database;

namespace DiscordBot.Core.Data
{
    public static class Data 
    {
        public static ulong GetStones(ulong UserId)
        {
            using (var DbContext = new SQLiteContext())
            {
                if (DbContext.Stoneses.Where(x => x.UserId == UserId).Count() < 1)
                {
                    return 0;
                }

                return DbContext.Stoneses.Where(x => x.UserId == UserId).Select(x => x.Amount).FirstOrDefault();
            }
        }

        public static async Task SaveStones(ulong UserId, ulong Amount)
        {
            using (var DbContext = new SQLiteContext())
            {
                if (DbContext.Stoneses.Where(x => x.UserId == UserId).Count() < 1)
                {
                    DbContext.Stoneses.Add(new Stones()
                    {
                        UserId = UserId,
                        Amount = Amount
                    });
                    
                }
                else
                {
                    Stones Current = DbContext.Stoneses.Where(x => x.UserId == UserId).FirstOrDefault();
                    Current.Amount += Amount;
                    DbContext.Stoneses.Update(Current);
                }

                await DbContext.SaveChangesAsync();

            }
        }
    }
}