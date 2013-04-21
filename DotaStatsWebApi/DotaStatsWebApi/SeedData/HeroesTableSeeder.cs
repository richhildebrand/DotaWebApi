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
            foreach (var hero in heroes)
            {
                hero.image_url = GetHeroImageUrl(hero);
                _db.Heroes.AddOrUpdate(hero);  
            }
            _db.SaveChanges();
        }
  
        private string GetHeroImageUrl(Hero hero)
        {
            var domain = "http://dotawebapi.apphb.com/";
            var imagePath = "Content/HeroPortraits/";
            var heroImage = hero.localized_name.Replace(" ", "_") + "_full.png";
            return domain + imagePath + heroImage;
        }
    }
}