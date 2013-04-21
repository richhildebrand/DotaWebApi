using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DotaStatsWebApi.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string account_id { get; set; }

        public Player() { }

        public Player(string accountId)
        {
            account_id = accountId;
        }
    }
}