using System;
using System.Data.Entity.Migrations;
using System.Linq;
using DotaStatsWebApi.Models;
using DotaStatsWebApi.Models.Steam;
using DotaStatsWebApi.Models.Steam.Heroes;
using DotaStatsWebApi.SteamApi;

namespace DotaStatsWebApi.SeedData
{
    public class HeroesTableSeeder
    {
        private readonly SteamApiConnector _webApi;
        private readonly AppHarborDB _db;

        public HeroesTableSeeder(SteamApiConnector webApi, AppHarborDB db)
        {
            _db = db;
            _webApi = webApi;
        }

        public void PopulateHeroesTable()
        {
            var steamHeroResult = _webApi.getHeroInfo();
            var heroes = steamHeroResult.result.heroes;
            heroes.ForEach(h => _db.Heroes.AddOrUpdate(h));
            _db.SaveChanges();
        }
    }
}