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

        public MatchSeeder(SteamApiConnector valveApi, AppHarborDB db)
        {
            _steamApi = valveApi;
            _db = db;
            _matchRepository = new MatchRepository(db);
        }

        public void PopulateMatchesFromPlayers()
        {
            var playerIds = _db.Players.Select(p => p.account_id).ToList();
            foreach (var playerId in playerIds)
	        {
                var playerMatches = _steamApi.TryGetPlayerMatchHistory(playerId);
                if (playerMatches != null)
                {
                    playerMatches = playerMatches.Take(5).ToList();
                    _matchRepository.SaveMatches(playerMatches);
                }
	        }
        }

        public void PopulateDetailsForMatches()
        {
            var allMatches = _db.Matches.OrderBy(m => m.duration).ToList();
            var matchCount = allMatches.Count();

            for (int i = 0; i < matchCount; i += 25)
            {
                var matches = allMatches.Skip(i).Take(25).ToList();
                foreach (var match in matches)
                {
                    PopulateDetailsForMatch(match);
                }
                _db.SaveChanges();
            }
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

        public void Populate5Matches()
        {
            var matches = _steamApi.Get25MostRecentMatches();
            matches = matches.Take(5).ToList();
            _matchRepository.SaveMatches(matches);
        }


    }
}