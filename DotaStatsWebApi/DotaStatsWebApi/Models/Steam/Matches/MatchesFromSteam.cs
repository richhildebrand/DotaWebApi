using System;
using System.Collections.Generic;
using System.Linq;

namespace DotaStatsWebApi.Models.Steam.Matches
{
    public class MatchesFromSteam
    {
        public int status { get; set; }
        public int num_results { get; set; }
        public int total_results { get; set; }
        public int results_remaining { get; set; }
        public List<Match> matches { get; set; }
    }
}