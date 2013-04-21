using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DotaStatsWebApi.Models;

namespace DotaStatsWebApi.Repositories
{
    public class PlayerRepository
    {
        private readonly AppHarborDB _db;

        public PlayerRepository(AppHarborDB db)
        {
            _db = db;
        }

        public void CreateAndAddPlayers(List<string> accountIds)
        {
            foreach (var accountId in accountIds)
            {
                CreateAndAddPlayer(accountId);
            }
        }

        public void CreateAndAddPlayer(string accountId)
        {
            var player = new Player(accountId);
            _db.Players.AddOrUpdate(player);
        }
    }
}