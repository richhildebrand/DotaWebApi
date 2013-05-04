namespace DotaStatsWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DotaStatsWebApi.Models;
    using DotaStatsWebApi.Repositories;
    using DotaStatsWebApi.SeedData;
    using DotaStatsWebApi.SteamApi;

    internal sealed class Configuration : DbMigrationsConfiguration<DotaStatsWebApi.Models.AppHarborDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DotaStatsWebApi.Models.AppHarborDB context)
        {
            //  This method will be called after migrating to the latest version.
            var webApi = new SteamApiConnector();
            var db = new AppHarborDB();

            var abilitySeeder = new AbilitySeeder(db);
            var itemSeeder = new ItemSeeder(db);
            var playerSeeder = new PlayerSeeder(webApi, db);
            var matchSeeder = new MatchSeeder(webApi, db);
            var heroSeeder = new HeroSeeder(webApi, db);
            var clanSeeder = new ClanSeeder(webApi, db);

            clanSeeder.PopulateClans();
            heroSeeder.PopulateHeroes();
            matchSeeder.Populate5Matches();

            playerSeeder.PopulatePlayersFromClanPlayers();
            playerSeeder.PopulatePlayersFromMatchPlayers();

            matchSeeder.PopulateMatchesFromPlayers();
            
            matchSeeder.PopulateDetailsForMatches();
            
            itemSeeder.PopulateItemsFromMatchPlayerItems();
            abilitySeeder.PopulateAbilitiesFromMatchPlayerAbilities();
            
            
        }
    }
}