using DatabaseAccessLibrary;
using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ControllerLibrary
{
    public class ItemCtrl
    {

        public IEnumerable<ModelLibrary.Item> GetItems()
        {
            var itemDb = new ItemDb();
            var items = itemDb.GetItems();
            var modelItems = new List<ModelLibrary.Item>();
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            PriceCtrl priceCtrl = new PriceCtrl();

            foreach (var item in items)
            {
                var CheckItem = db.Items.Single(a => a.id == item.id);
                var prices = db.Prices.Where(p => p.itemId == item.id).OrderByDescending(p => p.startDate);
                var price = prices.First();
                modelItems.Add(new ModelLibrary.Item
                {
                    Name = item.name,
                    Description = item.description,
                    ItemCat = ConvertItemCatToModel(CheckItem.ItemCat),
                    Id = item.id,
                    Price = priceCtrl.ConvertPriceToModel(price)
                });
            }

            return modelItems;
        }
        public ModelLibrary.Item GetItem(ModelLibrary.Item item)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            PriceCtrl priceCtrl = new PriceCtrl();
            var CheckItem = db.Items.Single(a => a.id == item.Id);
            var prices = db.Prices.Where(p => p.itemId == item.Id).OrderByDescending(p => p.startDate);
            var price = prices.First();


            ModelLibrary.Item returnItem = null;
            if (CheckItem != null)
            {

                returnItem = new ModelLibrary.Item
                {

                    Name = CheckItem.name,
                    Description = CheckItem.description,
                    ItemCat = ConvertItemCatToModel(CheckItem.ItemCat),
                    Id = CheckItem.id,
                    Price = priceCtrl.ConvertPriceToModel(price)

                };
                return returnItem;
            }
            else { return returnItem; }
        }

        public DatabaseAccessLibrary.Item CreateItem(ModelLibrary.Item item, int menuId)
        {
            var itemDb = new ItemDb();
            var returnItem = new DatabaseAccessLibrary.Item
            {
                
                name = item.Name,
                menuId = menuId,
                description = item.Description
                
            };

            var context = new ValidationContext(item, null, null);
            var result = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(item, context, result, true);
            if (!isModelStateValid)
                throw new ValidationException();

            itemDb.AddItem(returnItem);

            return returnItem;
        }

        public ModelLibrary.Item ConvertItemToModel(DatabaseAccessLibrary.Item dbItem)
        {
            var modelItem = new ModelLibrary.Item
            {
                Id = dbItem.id,
                Name = dbItem.name,
                Description = dbItem.description,
                ItemCat = ConvertItemCatToModel(dbItem.ItemCat)


            };
            return modelItem;
        }

        public DatabaseAccessLibrary.Item ConvertItemToDb(ModelLibrary.Item modelItem, int menuId) // remove statics
        {
            if (modelItem == null)
                return null;

            var dbItem = new DatabaseAccessLibrary.Item
            {
                menuId = menuId,
                id = modelItem.Id,
                name = modelItem.Name,
                description = modelItem.Description,
                ItemCat = ConvertItemCatToDb(modelItem.ItemCat)

            };
            return dbItem;
        }

        public DatabaseAccessLibrary.ItemCat CreateItemCat(ModelLibrary.ItemCat itemCat)
        {
            var itemCatDb = new ItemCatDb();
            var returnItemCat = new DatabaseAccessLibrary.ItemCat
            {
                name = itemCat.Name,
                id = itemCat.Id
            };

            itemCatDb.AddItemCat(returnItemCat);

            return returnItemCat;
        }
        public ModelLibrary.ItemCat ConvertItemCatToModel(DatabaseAccessLibrary.ItemCat dbItemCat)
        {
            var modelItemCat = new ModelLibrary.ItemCat();
            modelItemCat.Id = dbItemCat.id;
            modelItemCat.Name = dbItemCat.name;

            return modelItemCat;
        }

        public DatabaseAccessLibrary.ItemCat ConvertItemCatToDb(ModelLibrary.ItemCat modelItemCat)
        {
            if (modelItemCat == null)
                return null;
            var itemCat = new DatabaseAccessLibrary.ItemCat
            {
                id = modelItemCat.Id,
                name = modelItemCat.Name

            };
            return itemCat;

        }

        public void DeleteItem(ModelLibrary.Item item, int menuId)
        {
            var itemDb = new ItemDb();
            var dbItem = ConvertItemToDb(item, menuId);
            itemDb.DeleteItem(dbItem);
        }

        public void UpdateItem(ModelLibrary.Item beforeItem, ModelLibrary.Item afterItem, int menuId)
        {
            var itemDb = new ItemDb();

            var beforeDbItem = ConvertItemToDb(beforeItem, menuId);
            var afterDbItem = ConvertItemToDb(afterItem, menuId);
            itemDb.UpdateItem(beforeDbItem, afterDbItem);

        }
        public void DeleteItemCat(ModelLibrary.ItemCat itemCat)
        {
            var itemCatDb = new ItemCatDb();
            var dbItemCat = ConvertItemCatToDb(itemCat);
            itemCatDb.DeleteItemCat(dbItemCat);
        }

        public void UpdateItemCat(ModelLibrary.ItemCat beforeItemCat, ModelLibrary.ItemCat afterItemCat)
        {
            var itemCatDb = new ItemCatDb();

            var beforeDbItemCat = ConvertItemCatToDb(beforeItemCat);
            var afterDbItemCat = ConvertItemCatToDb(afterItemCat);
            itemCatDb.UpdateItemCat(beforeDbItemCat, afterDbItemCat);

        }

       //maybe get itemCats
    }
}
