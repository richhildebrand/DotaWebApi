using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotaStatsWebApi.Models;
using DotaStatsWebApi.Repositories;

namespace DotaStatsWebApi.SeedData
{
    public class PlayerSeeder
    {
        public void PopulatePlayersFromMatchPlayers()
        {
            var db = new AppHarborDB();
            var playerRepository = new PlayerRepository(db);

            var accountIds = new List<string>();
            foreach (var matchPlayer in db.MatchPlayers)
            {
                var accountId = matchPlayer.account_id;
                if (!accountIds.Contains(accountId))
                {
                    accountIds.Add(accountId);
                }
            }

            playerRepository.CreateAndAddPlayers(accountIds);
            db.SaveChanges();
        }
    }
}