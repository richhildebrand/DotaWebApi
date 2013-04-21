using System;
using System.Data.Entity;
using System.Linq;

namespace DotaStatsWebApi.Models
{
    public class AppHarborDB : DbContext
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Ability> Abilities { get; set; }

        public DbSet<MatchPlayer> MatchPlayers { get; set; }
        public DbSet<MatchPlayerItem> MatchPlayerItems { get; set; }
        public DbSet<MatchPlayerAbility> MatchPlayerAbilities { get; set; }
    }
}