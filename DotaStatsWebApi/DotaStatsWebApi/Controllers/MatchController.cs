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
    public class MatchController : ApiController
    {
        private readonly AppHarborDB _db;
        private readonly MatchRepository _matchRepository;

        public MatchController()
        {
            _db = new AppHarborDB();
            _matchRepository = new MatchRepository(_db);
        }

        [System.Web.Http.HttpGet]
        public Match Details(long id)
        {
            Match matches = _matchRepository.GetCompleteMatch(id);
            return matches;
        }
    }
}