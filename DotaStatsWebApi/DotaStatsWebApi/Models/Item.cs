using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DotaStatsWebApi.Models
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int ImageUrl { get; set; }

        public Item(int id)
        {
            Id = id;
        }

        public Item() { }
    }
}