using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using DotaStatsWebApi.Models;

namespace DotaStatsWebApi.Repositories
{
    public class AbilityRepository
    {
        private AppHarborDB _db;

        public AbilityRepository(AppHarborDB db)
        {
            _db = db;
        }



        public void CreateAndAddAbilities(Dictionary<int, int> heroAbilityIds)
        {
            foreach (var heroAbility in heroAbilityIds)
            {
                CreateAndAddAbility(heroAbility.Key, heroAbility.Value);
            }
        }

        public void CreateAndAddAbility(int abilityId, int heroId)
        {
            var ability = new Ability(abilityId, heroId);
            _db.Abilities.AddOrUpdate(ability);
        }
    }
}