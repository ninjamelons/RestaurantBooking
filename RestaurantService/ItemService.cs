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
        public void CreateItem(ModelLibrary.Item item)
        {
            itemCtrl.CreateItem(item);
            
        }

        public void CreateItemCat(ModelLibrary.ItemCat itemCat)
        {
            itemCtrl.CreateItemCat(itemCat);
        }

        public void DeleteItem(ModelLibrary.Item item)
        {
            var dbItem = itemCtrl.ConvertItemToDb(item);
            itemDb.DeleteItem(dbItem);
        }

        public void DeleteItemCat(ModelLibrary.ItemCat itemCat)
        {
            ItemCtrl itemCatCtrl = new ItemCtrl();
            var db = new JustFeastDbDataContext();
            
            if (itemCat.Id > 0)
            {
                var dbItemCat = itemCatCtrl.ConvertItemCatToDb(itemCat);
                var existing = db.ItemCats.Single(p => p.id == itemCat.Id);

                db.ItemCats.DeleteOnSubmit(existing);
            }
            
            db.SubmitChanges();

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

        public void UpdateItem(ModelLibrary.Item beforeItem, ModelLibrary.Item afterItem)
        {
            itemCtrl.UpdateItem(beforeItem, afterItem);
        }

        public void UpdateItemCat(ModelLibrary.ItemCat beforeItemCat, ModelLibrary.ItemCat afterItemCat)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            ItemCtrl itemCtrl = new ItemCtrl();
            itemCtrl.UpdateItemCat(beforeItemCat, afterItemCat);
        }

        public ModelLibrary.Price GetItemPrice(ModelLibrary.Item item)
        {
            ItemCtrl itemCtrl = new ItemCtrl();
            var itemo = itemCtrl.ConvertItemToDb(item);
            return itemCtrl.GetItemPrice(itemo);
            

        }

        public IEnumerable<ModelLibrary.ItemCat> GetItemCats()
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            ItemCtrl itemCtrl = new ItemCtrl();
            var res = db.ItemCats.ToList();
            List<ModelLibrary.ItemCat> modelItemCat = new List<ModelLibrary.ItemCat>();
            foreach (var x in res)
            {
                modelItemCat.Add(itemCtrl.ConvertItemCatToModel(x));
            }
            return modelItemCat;

        }

        public IEnumerable<ModelLibrary.Item> GetAllItemsByRestaurant(int restaurantId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var res = db.Items.Where(x => x.Menu.restaurantId == restaurantId).ToList();
            List<ModelLibrary.Item> modelItem = new List<ModelLibrary.Item>();
            foreach (var x in res)
            {
                modelItem.Add(itemCtrl.ConvertItemToModel(x));
            }
            return modelItem;
        }

        public ModelLibrary.Item GetItemByNameAndMenuId(string itemName, int menuId)
        {
            var itemCtrl = new ItemCtrl();
            var itemDb = new ItemDb();
            return itemCtrl.ConvertItemToModel(itemDb.GetItemByNameAndMenu(itemName, menuId));
        }

        public ModelLibrary.ItemCat GetItemCatByName(string name)
        {
            var itemCtrl = new ItemCtrl();
            var itemCatDb = new ItemCatDb();

            return itemCtrl.ConvertItemCatToModel(itemCatDb.GetItemCat(name));
        }
    }
}
