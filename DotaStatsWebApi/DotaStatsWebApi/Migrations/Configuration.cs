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

            var heroesTableSeeder = new HeroSeeder(webApi, db);
            heroesTableSeeder.PopulateHeroesTable();

            var matchRepository = new MatchRepository(db);
            var matchSeeder = new MatchSeeder(matchRepository, webApi, db);
            matchSeeder.Populate25Matches();
            matchSeeder.PopulateDetailsForMatches();

            var playerSeeder = new PlayerSeeder(webApi, db);
            playerSeeder.PopulatePlayersFromMatchPlayers();

            var itemSeeder = new ItemSeeder(db);
            itemSeeder.PopulateItemsFromMatchPlayerItems();

            var abilitySeeder = new AbilitySeeder(db);
            abilitySeeder.PopulateAbilitiesFromMatchPlayerAbilities();

            var clanSeeder = new ClanSeeder(db);
            clanSeeder.PopulateClansFromPlayers();
        }
    }
}