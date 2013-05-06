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
        private readonly MatchPlayerRepository _matchPlayerRepository;

        public PlayersController()
        {
            _db = new AppHarborDB();
            _matchPlayerRepository = new MatchPlayerRepository(_db);
        }

        [System.Web.Http.HttpGet]
        public List<Match> GetMatchHistory(string steamid32)
        {
            var matchPlayers =_db.MatchPlayers.Where(p => p.account_id == steamid32).ToList();
            var matches = new List<Match>();
            foreach (var matchPlayer in matchPlayers)
            {              
                _matchPlayerRepository.CompleteMatchPlayer(matchPlayer);
                var match = _db.Matches.First(m => m.match_id == matchPlayer.match_id);
                match.players = new List<MatchPlayer>();
                match.players.Add(matchPlayer);
                matches.Add(match);
            }
            return matches;
        }

        [System.Web.Http.HttpGet]
        public List<Player> GetPlayers()
        {
            return _db.Players.Take(25).ToList();
        }

        [System.Web.Http.HttpGet]
        public List<Player> SearchPlayers(string playerInfo)
        {
            return _db.Players.Where(p => p.account_id.Contains(playerInfo)
                                       || p.personaname.Contains(playerInfo))
                              .ToList();
        }

    }
}
