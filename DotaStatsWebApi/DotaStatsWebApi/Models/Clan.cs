using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DotaStatsWebApi.Models
{
    public class Clan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int team_id { get; set; }
        public string name { get; set;}
        public string tag { get; set;}
        public string time_created { get; set;}
        public string rating { get; set;}
        public string logo { get; set;}
        public string logo_sponsor { get; set;}
        public string country_code { get; set;}
        public string url { get; set;}
        public string games_played_with_current_roster { get; set;}
        public string admin_account_id { get; set; }

        //used by steam to show players in clans
        [NotMapped]
        public string player_0_account_id { get;set;}
        [NotMapped]
        public string player_1_account_id { get;set;}
        [NotMapped]
        public string player_2_account_id { get;set;}
        [NotMapped]
        public string player_3_account_id { get;set;}
        [NotMapped]
        public string player_4_account_id { get;set;}
        [NotMapped]
        public string player_5_account_id { get;set;}

        //used when sending clans to client
        [NotMapped]
        public List<Player> players { get; set; }

        public Clan() { }

        public Clan(int clanId)
        {
            team_id = clanId;
        }
    }
}