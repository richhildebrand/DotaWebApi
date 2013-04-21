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

            var heroesTableSeeder = new HeroesTableSeeder(webApi, db);
            heroesTableSeeder.PopulateHeroesTable();

            var matchPlayerAbilityRepository = new MatchPlayerAbilityRepository(db);
            var matchPlayerRepository = new MatchPlayerRepository(matchPlayerAbilityRepository, db);
            var matchRepository = new MatchRepository(matchPlayerRepository, db);

            var matchSeeder = new MatchSeeder(matchRepository, webApi, db);
            matchSeeder.Populate25Matches();
            matchSeeder.PopulateDetailsForMatches();

            var playerSeeder = new PlayerSeeder();
            playerSeeder.PopulatePlayersFromMatchPlayers();

            var itemSeeder = new ItemSeeder(db);
            itemSeeder.PopulateItemsFromMatchPlayerItems();

            var abilitySeeder = new AbilitySeeder(db);
            abilitySeeder.PopulateAbilitiesFromMatchPlayerAbilities();
        }
    }
}