using System;
using System.Data.Entity.Migrations;
using System.Linq;
using DotaStatsWebApi.Helpers;
using DotaStatsWebApi.Models;
using DotaStatsWebApi.Models.Steam;
using DotaStatsWebApi.Models.Steam.Heroes;
using DotaStatsWebApi.SteamApi;

namespace DotaStatsWebApi.SeedData
{
    public class HeroSeeder
    {
        private readonly SteamApiConnector _webApi;
        private readonly AppHarborDB _db;

        public HeroSeeder(SteamApiConnector webApi, AppHarborDB db)
        {
            _db = db;
            _webApi = webApi;
        }

        public void PopulateHeroes()
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
            var domain = UrlHelper.GetImageUrlBase();
            var imagePath = "HeroPortraits/";
            var heroImage = hero.localized_name.Replace(" ", "_") + "_full.png";
            return domain + imagePath + heroImage;
        }
    }
}