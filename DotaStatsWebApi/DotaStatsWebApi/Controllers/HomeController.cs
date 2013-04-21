using System;
using System.Linq;
using System.Web.Mvc;
using DotaStatsWebApi.Models;
using DotaStatsWebApi.Repositories;
using DotaStatsWebApi.SeedData;
using DotaStatsWebApi.SteamApi;

namespace DotaStatsWebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var webApi = new SteamApiConnector();
            var db = new AppHarborDB();

            var matchPlayerAbilityRepository = new MatchPlayerAbilityRepository(db);
            var matchPlayerRepository = new MatchPlayerRepository(matchPlayerAbilityRepository, db);
            var matchRepository = new MatchRepository(matchPlayerRepository, db);

            var matchSeeder = new MatchSeeder(matchRepository, webApi, db);
            matchSeeder.PopulateDetailsForMatches();

            return View();
        }
    }
}