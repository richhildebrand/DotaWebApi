namespace DotaStatsWebApi.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DotaStatsWebApi.Helpers;
    using DotaStatsWebApi.Models;
    using DotaStatsWebApi.SteamApi;

    public class ClanSeeder
    {
        private readonly AppHarborDB _db;
        private readonly SteamApiConnector _webApi;

        public ClanSeeder(SteamApiConnector webApi, AppHarborDB db)
        {
            _db = db;
            _webApi = webApi;
        }

        public void PopulateClans()
        {
            var clans = _webApi.GetClans();
            foreach (var clan in clans)
            {
                
            }

            clans.ForEach(c => _db.Clans.AddOrUpdate(c));    
            _db.SaveChanges();
        }

        private void TryLoadPlayer(string accountId)
        {
            
        }
    }
}