using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using DotaStatsWebApi.Models;

namespace DotaStatsWebApi.Repositories
{

    public class MatchPlayerRepository
    {
        private readonly AppHarborDB _db;
        private MatchPlayerAbilityRepository _matchPlayerAbilityRepository;

        public MatchPlayerRepository(AppHarborDB db)
        {            
            _db = db;
            _matchPlayerAbilityRepository = new MatchPlayerAbilityRepository(db);
        }

        public List<MatchPlayer> GetMatchPlayers(Match match)
        {
            var matchId = match.match_id;
            return _db.MatchPlayers.Where(mp => mp.match_id == matchId).ToList();
        }

        public void AddMatchPlayers(long matchId, List<MatchPlayer> matchPlayers)
        {
            foreach (var matchPlayer in matchPlayers)
            {
                AddMatchPlayer(matchId, matchPlayer);
            }
        }

        public void AddMatchPlayer(long matchId, MatchPlayer matchPlayer)
        {
            matchPlayer.match_id = matchId;
            matchPlayer.account_id = GetAccountId(matchPlayer.account_id);
            _matchPlayerAbilityRepository.AddMatchPlayerAbilities(matchPlayer);

            var matchPlayerItemRepository = new MatchPlayerItemRepository(matchPlayer, _db);
            matchPlayerItemRepository.AddMatchPlayerItems();

            _db.MatchPlayers.AddOrUpdate(matchPlayer);
        }
  
        private string GetAccountId(string account_id)
        {
            if (account_id == null) return "Guest";
            else if (account_id == "4294967295") return "Anonymous";
            else return account_id;
        }
    }
}