using DataAccess;
using Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.SplitLogic
{
    public class ItemLogic
    {
        public Item GetItem(Item item)
        {
            return Data.ItemData.GetAllItems().Where(i => i == item).FirstOrDefault();
        }

        public List<Item> GetShopItemNames()
        {
            return Data.ItemData.GetAllItems();
        }
    }
}
