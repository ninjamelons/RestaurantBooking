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
        ItemDb itemDb = new ItemDb();
        ItemCatDb itemCatDb = new ItemCatDb();
        ItemCtrl itemCtrl = new ItemCtrl();
        public void CreateItem(ModelLibrary.Item item, int menuId)
        {
            itemCtrl.CreateItem(item,menuId);
            
        }

        public void CreateItemCat(ModelLibrary.ItemCat itemCat)
        {
            itemCtrl.CreateItemCat(itemCat);
        }

        public void DeleteItem(ModelLibrary.Item item, int menuId)
        {
            var dbItem = itemCtrl.ConvertItemToDb(item, menuId);
            itemDb.DeleteItem(dbItem);
        }

        public void DeleteItemCat(ModelLibrary.ItemCat itemCat)
        {

            var dbItemCat = itemCtrl.ConvertItemCatToDb(itemCat);
            itemCatDb.DeleteItemCat(dbItemCat);
        }

        public IEnumerable<ModelLibrary.ItemCat> GetAllItemCategories()
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var itemCat = db.ItemCats.ToList();
            List<ModelLibrary.ItemCat> modelItemCats = new List<ModelLibrary.ItemCat>();
            foreach (var a in itemCat)
            {
                modelItemCats.Add(itemCtrl.ConvertItemCatToModel(a));
            }
            return modelItemCats;
        }

        public IEnumerable<ModelLibrary.Item> GetAllItemsByMenu(int menuId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var res = db.Items.Where(x => x.menuId == menuId).ToList();
            List<ModelLibrary.Item> modelItem = new List<ModelLibrary.Item>();
            foreach (var x in res)
            {
                modelItem.Add(itemCtrl.ConvertItemToModel(x));
            }
            return modelItem;

        }

        public IEnumerable<ModelLibrary.Item> GetAllItemsByCategory(int categoryId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var res = db.Items.Where(x => x.itemCatId == categoryId).ToList();
            List<ModelLibrary.Item> modelItem = new List<ModelLibrary.Item>();
            foreach (var x in res)
            {
                modelItem.Add(itemCtrl.ConvertItemToModel(x));
            }
            return modelItem;
        }

        public void UpdateItem(ModelLibrary.Item beforeItem, ModelLibrary.Item afterItem, int menuId)
        {
            itemCtrl.UpdateItem(beforeItem, afterItem, menuId);
        }

        public void UpdateItemCat(ModelLibrary.ItemCat beforeItemCat, ModelLibrary.ItemCat afterItemCat)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            ItemCtrl itemCtrl = new ItemCtrl();
            itemCtrl.UpdateItemCat(beforeItemCat, afterItemCat);
        }
    }
}
