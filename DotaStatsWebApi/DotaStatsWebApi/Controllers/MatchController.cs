using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DotaStatsWebApi.Models;
using Newtonsoft.Json;

namespace DotaStatsWebApi.Controllers
{
    public class MatchController : ApiController
    {
        private readonly AppHarborDB _db;

        public MatchController()
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
