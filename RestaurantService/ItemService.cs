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
        public void CreateItem(ModelLibrary.Item item, int menuId, int itemCatId)
        {
            itemCtrl.CreateItem(item, menuId, itemCatId);
        }

        public void CreateItemCat(ModelLibrary.ItemCat itemCat)
        {
            itemCtrl.CreateItemCat(itemCat);
        }
        public ModelLibrary.Item GetItem(int itemId)
        {
            ItemCtrl itemCtrl = new ItemCtrl();
            return itemCtrl.GetItem(itemId);
        }

        public void DeleteItem(int itemId)
        {

            itemDb.DeleteItem(itemId);
        }

        public void DeleteItemCat(int itemCatId)
        {
            ItemCtrl itemCatCtrl = new ItemCtrl();
            itemCatCtrl.DeleteItemCat(itemCatId);

        }

        public IEnumerable<ModelLibrary.ItemCat> GetAllItemCategories()
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var res = db.ItemCats.ToList();
            List<ModelLibrary.ItemCat> mRes = new List<ModelLibrary.ItemCat>();
            foreach (var x in res)
            {
                mRes.Add(itemCtrl.ConvertItemCatToModel(x));
            }
            return mRes;

        }

        public IEnumerable<ModelLibrary.Item> GetAllItemsByMenu(int menuId)
        {
            return itemCtrl.GetMenuItems(menuId);
            

        }

        public IEnumerable<ModelLibrary.Item> GetAllItemsByCategory(int categoryId)
        {
            return (IEnumerable<ModelLibrary.Item>)itemDb.GetCategoryItems(categoryId); // is this legal?
        }

        public void UpdateItem(ModelLibrary.Item updatedItem, int menuId, int itemCatId)
        {
            itemCtrl.UpdateItem(updatedItem, menuId, itemCatId);
        }

        public void UpdateItemCat(ModelLibrary.ItemCat beforeItemCat, ModelLibrary.ItemCat afterItemCat)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            ItemCtrl itemCtrl = new ItemCtrl();
            itemCtrl.UpdateItemCat(beforeItemCat, afterItemCat);
        }

        public ModelLibrary.Price GetItemPrice(ModelLibrary.Item item, int menuId, int itemCatId)
        {
            ItemCtrl itemCtrl = new ItemCtrl();
            var itemo = itemCtrl.ConvertItemToDb(item, menuId, itemCatId);
            return itemCtrl.GetItemPrice(1000000);
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

        public ModelLibrary.ItemCat GetItemCatById(int id)
        {
            var itemCtrl = new ItemCtrl();
            var itemCatDb = new ItemCatDb();

            return itemCtrl.ConvertItemCatToModel(itemCatDb.GetItemCatById(id));
        }

        public ModelLibrary.Item GetItemByName(string itemName)
        {
            var itemCtrl = new ItemCtrl();
            var itemDb = new ItemDb();
            return itemCtrl.ConvertItemToModelWithoutPrice(itemDb.GetItemByName(itemName));
        }

        public ModelLibrary.ItemCat GetItemCatByName(string itemCatName)
        {
            return itemCtrl.GetItemCatByName(itemCatName);
        }

        public ModelLibrary.ItemCat GetCatByItemCatId(int itemId)
        {
            return itemCtrl.GetItemCatByItemCatId(itemId);
        }
    }
}
