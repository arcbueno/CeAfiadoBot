using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace DiscordBot.Resources.Database
{
    public class SQLiteContext : DbContext
    {
        public DbSet<Stones> Stoneses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            string DbLocation = Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\netcoreapp2.1",@"Data\Database.sqlite");
            Options.UseSqlite("Data Source= " + DbLocation);
            
        }
    }
}