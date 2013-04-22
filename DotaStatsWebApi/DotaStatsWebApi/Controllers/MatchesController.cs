using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using DotaStatsWebApi.Models;
using Newtonsoft.Json;

namespace DotaStatsWebApi.Controllers
{
    public class MatchesController : ApiController
    {
        private readonly AppHarborDB _db;

        public MatchesController()
        {
            _db = new AppHarborDB();
        }

        public List<Match> GetRecentMatches()
        {
            var matches = _db.Matches.ToList();
            return matches;
        }

    }
}
