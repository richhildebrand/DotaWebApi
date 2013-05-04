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

            heroSeeder.PopulateHeroesTable();

            clanSeeder.PopulateClans();
            //matchSeeder.Populate25Matches();
            matchSeeder.Populate5Matches();
            matchSeeder.PopulateDetailsForMatches();

            playerSeeder.PopulatePlayersFromClanPlayers();
            playerSeeder.PopulatePlayersFromMatchPlayers();

            itemSeeder.PopulateItemsFromMatchPlayerItems();
            abilitySeeder.PopulateAbilitiesFromMatchPlayerAbilities();
            
            
        }
    }
}