using System;
using System.Data.Entity.Migrations;
using System.Linq;
using DotaStatsWebApi.Models;

namespace DotaStatsWebApi.Repositories
{
    public class MatchPlayerAbilityRepository
    {
        private readonly AppHarborDB _db;

        public MatchPlayerAbilityRepository(AppHarborDB db)
        {
            _db = db;
        }

        public void AddMatchPlayerAbilities(MatchPlayer matchPlayer)
        {
            if (matchPlayer.ability_upgrades != null)
            {
                foreach (var ability in matchPlayer.ability_upgrades)
                {
                    ability.match_id = matchPlayer.match_id;
                    ability.account_id = matchPlayer.account_id;
                    ability.player_slot = matchPlayer.player_slot;
                    _db.MatchPlayerAbilities.AddOrUpdate(ability);
                }
            }
        }
    }
}