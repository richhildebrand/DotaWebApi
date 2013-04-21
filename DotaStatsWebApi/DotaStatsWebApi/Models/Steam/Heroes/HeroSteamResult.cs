using System;
using System.Linq;
using DotaStatsWebApi.Models;
using DotaStatsWebApi.Models.Steam.Heroes;

namespace DotaStatsWebApi.Models.Steam.Heroes
{
    public class HeroSteamResult
    {
        public HeroesFromSteam result { get; set; }
    }
}