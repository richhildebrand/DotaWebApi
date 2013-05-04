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
            var accountIds = _db.ClanPlayers.Where(cp => cp.ClanId == clan.team_id)
                                            .Select(cp => cp.AccountId)
                                            .ToList();

            var clanMembers = new List<Player>();
            foreach (var accountId in accountIds)
            {
                string id = accountId.ToString();
                var member = _db.Players.FirstOrDefault(p => p.account_id == id);
                clanMembers.Add(member);
            }

            clan.players = clanMembers;
            return clan;
        }

    }
}
