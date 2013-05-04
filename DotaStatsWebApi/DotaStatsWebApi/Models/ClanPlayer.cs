using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DotaStatsWebApi.Models
{
    public class ClanPlayer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 0)]
        public int ClanId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 1)]
        public int AccountId { get; set; }

        [NotMapped]
        public Player player { get; set; }
        
        public ClanPlayer() { }

        public ClanPlayer(int accountId, int clanId)
        {
            ClanId = clanId;
            AccountId = accountId;
        }


    }
}