using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotaStatsWebApi.Models
{
    public class Ability
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int AbilityId { get; set; }
        public int HeroId { get; set; }
        public int ImageUrl { get; set; }

        public Ability(int abilityId, int heroId)
        {
            AbilityId = abilityId;
            HeroId = heroId;
        }

        public Ability()
        {
        }
    }
}