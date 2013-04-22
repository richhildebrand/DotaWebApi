using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using DotaStatsWebApi.Models;
using DotaStatsWebApi.Repositories;
using Newtonsoft.Json;

namespace DotaStatsWebApi.Controllers
{
    public class MatchesController : ApiController
    {
        private readonly AppHarborDB _db;
        private readonly MatchRepository _matchRepository;

        public MatchesController()
        {
            _db = new AppHarborDB();
            _matchRepository = new MatchRepository(_db);
        }

        [System.Web.Http.HttpGet]
        public List<Match> GetRecentMatches()
        {
            List<Match> matches = _matchRepository.Get25CompleteMatches();
            return matches;
        }

        [System.Web.Http.HttpGet]
        public Match GetMatchDetails(long matchId)
        {
            Match matches = _matchRepository.GetCompleteMatch(matchId);
            return matches;
        }

    }
}
