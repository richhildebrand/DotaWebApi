using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DotaStatsWebApi.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string account_id { get; set; }
        public string steamid { get; set; }
        public string communityvisibilitystate { get; set; }
        public string profilestate { get; set; }
        public string personaname { get; set; }
        public string lastlogoff { get; set; }
        public string profileurl { get; set; }
        public string avatar { get; set; }
        public string avatarmedium { get; set; }
        public string avatarfull { get; set; }
        public string personastate { get; set; }
        public int primaryclanid { get; set; }
        public string timecreated { get; set; }

        public Player() { }

        public Player(string accountId)
        {
            account_id = accountId;
        }
    }
}