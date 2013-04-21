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
            return View();
        }
    }
}