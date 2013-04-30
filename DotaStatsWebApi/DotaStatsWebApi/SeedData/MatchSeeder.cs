using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DotaStatsWebApi.Models;
using DotaStatsWebApi.Repositories;
using DotaStatsWebApi.SteamApi;

namespace DotaStatsWebApi.SeedData
{
    public class MatchSeeder
    {
        private readonly SteamApiConnector _steamApi;
        private readonly AppHarborDB _db;

        private readonly MatchRepository _matchRepository;

        public MatchSeeder(MatchRepository matchRepository, SteamApiConnector valveApi, AppHarborDB db)
        {
            _matchRepository = matchRepository;
            _db = db;
            _steamApi = valveApi;
        }

        public void PopulateDetailsForMatches()
        {
            var matches = _db.Matches.ToList();

            foreach (var match in matches)
            {
                PopulateDetailsForMatch(match);
            }
            _db.SaveChanges();
        }

        public void PopulateDetailsForMatches(List<Match> matches)
        {
            foreach (var match in matches)
            {
                PopulateDetailsForMatch(match);
            }
            _db.SaveChanges();
        }

        public void PopulateDetailsForMatch(Match match)
        {
            var matchDetails = _steamApi.TryGetMatchDetails(match.match_id);
            if (matchDetails != null)
            {
                _matchRepository.AddMatch(matchDetails);
            }
        }


        public void Populate25Matches()
        {
            var matches = _steamApi.Get25MostRecentMatches();
            _matchRepository.SaveMatches(matches);
        }


    }
}