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

    }
}
