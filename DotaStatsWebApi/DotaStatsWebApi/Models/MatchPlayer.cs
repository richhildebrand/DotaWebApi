using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DotaStatsWebApi.Models
{
    public class MatchPlayer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 0)]
        public long match_id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 1)]
        public string account_id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 2)]
        public int player_slot { get; set; }

        public int kills { get; set; }
        public int deaths { get; set; }
        public int assists { get; set; }
        public int leaver_status { get; set; }
        public int gold { get; set; }
        public int last_hits { get; set; }
        public int denies { get; set; }
        public int gold_per_min { get; set; }
        public int xp_per_min { get; set; }
        public int gold_spent { get; set; }
        public int hero_damage { get; set; }
        public int tower_damage { get; set; }
        public int level { get; set; }

        public List<MatchPlayerAbility> ability_upgrades { get; set; }

        // TODO: add items to MatchPlayer
        [NotMapped]
        public int item_0 { get; set; }
        [NotMapped]
        public int item_1 { get; set; }
        [NotMapped]
        public int item_2 { get; set; }
        [NotMapped]
        public int item_3 { get; set; }
        [NotMapped]
        public int item_4 { get; set; }
        [NotMapped]
        public int item_5 { get; set; }
     
        public int hero_id { get; set; }
        
        // Used when returning data
        [NotMapped]
        public Hero hero { get; set; }
        [NotMapped]
        public Player playerInformation { get; set; }
    }
}