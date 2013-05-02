namespace DotaStatsWebApi.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DotaStatsWebApi.Helpers;
    using DotaStatsWebApi.Models;

    public class ClanSeeder
    {
        private readonly AppHarborDB _db;
        private readonly ClanRepository _clanRepository;

        public ClanSeeder(AppHarborDB db)
        {
            _db = db;
            _clanRepository = new ClanRepository(db);
        }

        public void PopulateClansFromPlayers()
        {
            var clansIds = _db.Players.Where(p => p.primaryclanid != 0)
                                      .Select(p => p.primaryclanid)
                                      .Distinct().ToList();
            var clans = _clanRepository.CreateClans(clansIds);
            clans.ForEach(c => _db.Clans.AddOrUpdate(c));
            _db.SaveChanges();
        }
    }
  
    public class ClanRepository
    {
        private AppHarborDB _db;
        private readonly ClanHelper _clanHelper;

        public ClanRepository(AppHarborDB db)
        {
            _db = db;
            _clanHelper = new ClanHelper();
        }

        public List<Clan> CreateClans(List<int> clanIds)
        {
            var clans = new List<Clan>();
            foreach (var clanId in clanIds)
            {
                var clan = new Clan(clanId);
                clan.ImageUrl = _clanHelper.GenrateClanImageUrl(clanId);
                clans.Add(clan);
            }
            return clans;
        }
    }
}