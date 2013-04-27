using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DotaStatsWebApi.Models;

namespace DotaStatsWebApi.Repositories
{
    public class MatchPlayerItemRepository
    {
        private readonly AppHarborDB _db;
        private readonly MatchPlayer _matchPlayer;

        public MatchPlayerItemRepository(MatchPlayer matchPlayer, AppHarborDB db)
        {
            _matchPlayer = matchPlayer;
            _db = db;
        }

        public List<MatchPlayerItem> GetItems()
        {
            var items = _db.MatchPlayerItems.Where(mpi => mpi.account_id == _matchPlayer.account_id
                                           && mpi.player_slot == _matchPlayer.player_slot
                                           && mpi.match_id == _matchPlayer.match_id).ToList();
            foreach (var item in items)
            {
                item.item = _db.Items.FirstOrDefault(i => i.Id == item.ItemId);
            }
            return items.ToList();
        }

        public void AddMatchPlayerItems()
        {
            var matchPlayerItems = BuildMatchPlayerItemList();
            foreach (var item in matchPlayerItems)
            {
                _db.MatchPlayerItems.AddOrUpdate(item);
            }
        }

        private List<MatchPlayerItem> BuildMatchPlayerItemList()
        {
            var items = new List<MatchPlayerItem>();
            if (_matchPlayer.item_0 != 0) 
                items.Add(BuildMatchPlayerItem(_matchPlayer.item_0, 0));
            if (_matchPlayer.item_1 != 0) 
                items.Add(BuildMatchPlayerItem(_matchPlayer.item_1, 1));
            if (_matchPlayer.item_2 != 0)
                items.Add(BuildMatchPlayerItem(_matchPlayer.item_2, 2));
            if (_matchPlayer.item_3 != 0)
                items.Add(BuildMatchPlayerItem(_matchPlayer.item_3, 3));
            if (_matchPlayer.item_4 != 0)
                items.Add(BuildMatchPlayerItem(_matchPlayer.item_4, 4));
            if (_matchPlayer.item_5 != 0)
                items.Add(BuildMatchPlayerItem(_matchPlayer.item_5, 5));
            return items;
        }

        private MatchPlayerItem BuildMatchPlayerItem(int itemId, int itemSlot)
        {

            var item = new MatchPlayerItem();
            item.match_id = _matchPlayer.match_id;
            item.account_id = _matchPlayer.account_id;
            item.player_slot = _matchPlayer.player_slot;
            item.ItemId = itemId;
            item.ItemSlot = itemSlot;
            return item;
        }
    }
}