using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DotaStatsWebApi.Models
{
    public class Clan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        
        public Clan(int clanId)
        {
            Id = clanId;
        }
    }
}