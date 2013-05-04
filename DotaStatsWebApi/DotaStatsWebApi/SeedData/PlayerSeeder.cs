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
        private PlayerRepository _playerRepository;

        public PlayerSeeder(SteamApiConnector webApi, AppHarborDB db)
        {
            _webApi = webApi;
            _db = db;
            _playerRepository = new PlayerRepository(db);
        }

        public void PopulatePlayersFromClanPlayers()
        {
            var clanPlayerIds = _db.ClanPlayers.Select(p => p.AccountId)
                                               .Distinct()
                                               .ToList();
            AddPlayers(clanPlayerIds);
            _db.SaveChanges();
        }

        private void AddPlayers(List<string> accountIds)
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
        }

        public void PopulatePlayersFromMatchPlayers()
        {
            var matchPlayers = _db.MatchPlayers.ToList();

            var accountIds = new List<string>();
            //Avoid trying to populate these ids
            accountIds.Add("Guest");
            accountIds.Add("Anonymous");

            foreach (var matchPlayer in matchPlayers)
            {
                var accountId = matchPlayer.account_id;
                if (!accountIds.Contains(accountId))
                {
                    accountIds.Add(accountId);
                    var player = _webApi.TryGetPlayerInfo((accountId));
                    if (player != null)
                    {
                        player.account_id = accountId;
                        _db.Players.AddOrUpdate(player);
                    }
                }
            }

            //playerRepository.CreateAndAddPlayers(accountIds);
            _db.SaveChanges();
        }
    }
}