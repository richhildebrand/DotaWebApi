using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DotaStatsWebApi.Models;

namespace DotaStatsWebApi.Repositories
{
    public class ItemRepository
    {
        private readonly AppHarborDB _db;

        public ItemRepository(AppHarborDB db)
        {
            _db = db;
        }

        public void CreateAndAddItems(List<int> itemIds)
        {
            foreach (var item in itemIds)
            {
                CreateAndAddItem(item);
            }
        }

        public void CreateAndAddItem(int itemId)
        {
            var item = new Item(itemId);
            _db.Items.AddOrUpdate(item);
        }
    }
}