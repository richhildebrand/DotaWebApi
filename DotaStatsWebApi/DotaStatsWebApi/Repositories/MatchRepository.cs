using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DotaStatsWebApi.Models;

namespace DotaStatsWebApi.Repositories
{
    public class MatchRepository
    {
        private readonly AppHarborDB _db;
        private readonly MatchPlayerRepository _matchPlayerRepository;

        public MatchRepository(MatchPlayerRepository matchPlayerRepository, AppHarborDB db)
        {
            _matchPlayerRepository = matchPlayerRepository;
            _db = db;
        }

        public void SaveMatches(List<Match> matches)
        {
            foreach (var match in matches)
            {
                AddMatch(match);
            }
            _db.SaveChanges();
        }

        public void AddMatch(Match match)
        {
            _matchPlayerRepository.AddMatchPlayers(match.match_id, match.players);
            _db.Matches.AddOrUpdate(match);
        }
    }
}