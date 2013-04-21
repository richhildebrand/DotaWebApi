using System.Collections.Generic;
using DotaStatsWebApi.Models;
using DotaStatsWebApi.Repositories;

namespace DotaStatsWebApi.SeedData
{
    public class ItemSeeder
    {
        private readonly AppHarborDB _db;
        private readonly ItemRepository _itemRepository;

        public ItemSeeder(AppHarborDB db)
        {
            _db = db;
            _itemRepository = new ItemRepository(db);
        }

        public void PopulateItemsFromMatchPlayerItems()
        {
            var itemIds = new List<int>();
            foreach (var matchPlayerItem in _db.MatchPlayerItems)
            {
                var itemId = matchPlayerItem.ItemId;
                if (!itemIds.Contains(itemId))
                {
                    itemIds.Add(itemId);
                }
            }

            _itemRepository.CreateAndAddItems(itemIds);
            _db.SaveChanges();
        }
    }
}