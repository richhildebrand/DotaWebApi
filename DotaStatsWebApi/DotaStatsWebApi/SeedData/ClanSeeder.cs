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
            var accountIds = new List<string>();
            foreach (var clan in clans)
            {
                TryAddClanPlayer(clan.player_0_account_id, clan.team_id);
                TryAddClanPlayer(clan.player_1_account_id, clan.team_id);
                TryAddClanPlayer(clan.player_2_account_id, clan.team_id);
                TryAddClanPlayer(clan.player_3_account_id, clan.team_id);
                TryAddClanPlayer(clan.player_4_account_id, clan.team_id);
                TryAddClanPlayer(clan.player_5_account_id, clan.team_id);
            }
            clans.ForEach(c => _db.Clans.AddOrUpdate(c));    
            _db.SaveChanges();
        }

        private void TryAddClanPlayer(string accountId, int clanId)
        {
            try
            {
                int.Parse(accountId);
                var clanPlayer = new ClanPlayer(accountId, clanId);
                _db.ClanPlayers.AddOrUpdate(clanPlayer);
            }
            catch { }
        }
    }
}