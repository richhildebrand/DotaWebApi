using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DotaStatsWebApi.Models;

namespace DotaStatsWebApi.Controllers
{
    public class ClansController : ApiController
    {
        private AppHarborDB _db;

        public ClansController()
        {
            _db = new AppHarborDB();
        }

        [System.Web.Http.HttpGet]
        public List<Clan> GetClans()
        {
            return _db.Clans.ToList();
        }
        
        [System.Web.Http.HttpGet]
        public Clan GetClanDetails(int clanId)
        {
            var clan =_db.Clans.FirstOrDefault(c => c.team_id == clanId);
            var clanPlayers = _db.ClanPlayers.Where(cp => cp.ClanId == clan.team_id)
                                             .ToList();

            foreach (var clanPlayer in clanPlayers)
            {
                var player = _db.Players.FirstOrDefault(p => p.account_id == clanPlayer.AccountId);
                clanPlayer.player = player;
            }

            clan.clanPlayers = clanPlayers;
            return clan;
        }

    }
}
