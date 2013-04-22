using System;
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

        public ContentResult GetRecentMatches()
        {
            var matches = _db.Matches.ToList();
            var matchJson = JsonConvert.SerializeObject(matches);
            return new ContentResult { Content = matchJson, ContentType = "application/json" };
        }

    }
}
