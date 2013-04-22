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

        public MatchRepository(AppHarborDB db)
        {
            _db = db;
            _matchPlayerRepository = new MatchPlayerRepository(db);
        }

        public Match GetCompleteMatch(long id)
        {
            var match = _db.Matches.FirstOrDefault(m => m.match_id == id);
            var matchPlayers = _matchPlayerRepository.GetMatchPlayers(match);
            match.players = matchPlayers;
            return match;
        }

        public List<Match> Get25CompleteMatches()
        {
            var matches = _db.Matches.Take(25).ToList();
            foreach (var match in matches)
            {
                var matchPlayers = _matchPlayerRepository.GetMatchPlayers(match);
                match.players = matchPlayers;
            }
            return matches;
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