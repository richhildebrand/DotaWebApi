using System;
using System.Collections.Generic;
using System.Linq;
using DotaStatsWebApi.Models;
using DotaStatsWebApi.Repositories;

namespace DotaStatsWebApi.SeedData
{
    public class AbilitySeeder
    {
        private AppHarborDB _db;
        private AbilityRepository _abilityRepository;

        public AbilitySeeder(AppHarborDB db)
        {
            _db = db;
            _abilityRepository = new AbilityRepository(db);
        }

        public void PopulateAbilitiesFromMatchPlayerAbilities()
        {
            var heroAbilityIds = new Dictionary<int, int>();
            var matchPlayerAbilites = _db.MatchPlayerAbilities.ToList();
            var matchPlayers = _db.MatchPlayers.ToList() ;
            foreach (var matchPlayerAbility in matchPlayerAbilites)
            {
                var heroAbilityId = matchPlayerAbility.ability;
                if (!heroAbilityIds.ContainsKey(heroAbilityId))
                {
                    var matchPlayer = matchPlayers.First(mp => mp.account_id == matchPlayerAbility.account_id &&
                                                              mp.match_id == matchPlayerAbility.match_id &&
                                                              mp.player_slot == matchPlayerAbility.player_slot);
                    
                    heroAbilityIds.Add(heroAbilityId, matchPlayer.hero_id);
                }
            }
            _abilityRepository.CreateAndAddAbilities(heroAbilityIds);
            _db.SaveChanges();
        }
    }
  
}