using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DotaStatsWebApi.Models;
using DotaStatsWebApi.Repositories;
using DotaStatsWebApi.SteamApi;

namespace DotaStatsWebApi.SeedData
{
    public class PlayerSeeder
    {
        private readonly SteamApiConnector _webApi;
        private readonly AppHarborDB _db;

        public PlayerSeeder(SteamApiConnector webApi, AppHarborDB db)
        {
            _webApi = webApi;
            _db = db;
        }

        public void PopulatePlayersFromClanPlayers()
        {
            var clanPlayerIds = _db.ClanPlayers.Select(p => p.AccountId)
                                               .Distinct()
                                               .ToList();
            AddAndSavePlayers(clanPlayerIds);
        }

        public void PopulatePlayersFromMatchPlayers()
        {
            var matchPlayerIds = _db.MatchPlayers.Select(mp => mp.account_id)
                                                .Distinct()
                                                .ToList();
            AddAndSavePlayers(matchPlayerIds);
        }

        private void AddAndSavePlayers(List<string> accountIds)
        {
            foreach (var accountId in accountIds)
            {
                var player = _webApi.TryGetPlayerInfo(accountId);
                if (player != null)
                {
                    player.account_id = accountId;
                    _db.Players.AddOrUpdate(player);
                }
            }
            _db.SaveChanges();
        }
    }
}