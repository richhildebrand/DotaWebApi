using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DotaStatsWebApi.Models;
using DotaStatsWebApi.Repositories;

namespace DotaStatsWebApi.Controllers
{
    public class PlayersController : ApiController
    {
        private readonly AppHarborDB _db;
        private readonly MatchRepository _matchRepository;

        public PlayersController()
        {
            _db = new AppHarborDB();
            _matchRepository = new MatchRepository(_db);
        }

        [System.Web.Http.HttpGet]
        public List<Match> GetMatchHistory(string steamid32)
        {
            var matchPlayers =_db.MatchPlayers.Where(p => p.account_id == steamid32).ToList();
            var matches = new List<Match>();
            foreach (var matchPlayer in matchPlayers)
            {
                var matchId = matchPlayer.match_id;
                var match = _matchRepository.GetCompleteMatch(matchId);
                matches.Add(match);
            }
            return matches;
        }

    }
}
