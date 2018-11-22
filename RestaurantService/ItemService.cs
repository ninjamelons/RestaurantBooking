using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ModelLibrary;
using DatabaseAccessLibrary;
using ControllerLibrary;

namespace RestaurantService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ItemService" in both code and config file together.
    public class ItemService : IItemService
    {
        public void CreateItem(ModelLibrary.Item item)
        {
            var dbItem = ItemCtrl.ConvertItemToDb(item);
            ItemDb.AddItem(dbItem);
            
        }

        public void CreateItemCat(ModelLibrary.ItemCat itemCat)
        {
            var dbItemCat = ItemCtrl.ConvertItemCatToDb(itemCat);
            ItemCatDb.AddItemCat(dbItemCat);
        }

        public void DeleteItem(ModelLibrary.Item item)
        {
            var dbItem = ItemCtrl.ConvertItemToDb(item);
            ItemDb.DeleteItem(dbItem);
        }

        public void DeleteItemCat(ModelLibrary.ItemCat itemCat)
        {
            var dbItemCat = ItemCtrl.ConvertItemCatToDb(itemCat);
            ItemCatDb.DeleteItemCat(dbItemCat);
        }

        public IEnumerable<ModelLibrary.ItemCat> GetAllItemCategories()
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var itemCat = db.ItemCats.ToList();
            List<ModelLibrary.ItemCat> modelItemCats = new List<ModelLibrary.ItemCat>();
            foreach (var a in itemCat)
            {
                modelItemCats.Add(ItemCtrl.ConvertItemCatToModel(a));
            }
            return modelItemCats;
        }

        public IEnumerable<ModelLibrary.Item> GetAllItems()
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var item = db.Items.ToList();
            List<ModelLibrary.Item> modelItems = new List<ModelLibrary.Item>();
            foreach(var a in item)
            {
                modelItems.Add(ItemCtrl.ConvertItemToModel(a));
            }
            return modelItems;
            
        }

        public IEnumerable<ModelLibrary.Item> GetAllItemsByCategory(int categoryId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var items = db.Items.Where(a => a.itemCatId == categoryId).ToList();
            List<ModelLibrary.Item> modelItem = new List<ModelLibrary.Item>();
            foreach (var a in items)
            {
                modelItem.Add(ItemCtrl.ConvertItemToModel(a));
            }
            return modelItem;
        }

        public IEnumerable<ModelLibrary.Item> GetAllItemsByMenu(int menuId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var items = db.Items.Where(a => a.itemMenuId == menuId).ToList();
            List<ModelLibrary.Item> modelItem = new List<ModelLibrary.Item>();
            foreach (var a in items)
            {
                modelItem.Add(ItemCtrl.ConvertItemToModel(a));
            }
            return modelItem;
        }

        public void UpdateItem(ModelLibrary.Item item)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var aitem = db.Items.SingleOrDefault(a => a.id == item.Id);
            aitem = ItemCtrl.ConvertItemToDb(item);
            db.SubmitChanges();
        }

        public void UpdateItemCat(ModelLibrary.ItemCat itemCat)
        {
            throw new NotImplementedException();
        }
    }
}
