using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DotaStatsWebApi.Models
{
    public class Hero
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string localized_name { get; set; }
        public string image_url { get; set; }

        public Hero(int heroId)
        {
        }

        public Hero() { }
    }
}