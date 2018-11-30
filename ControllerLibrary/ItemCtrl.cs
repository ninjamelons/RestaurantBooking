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
        public ModelLibrary.Item GetItem2(ModelLibrary.Item item)
        {
            ItemCtrl itemCtrl = new ItemCtrl();
            PriceCtrl priceCtrl = new PriceCtrl();
            MenuCtrl menuCtrl = new MenuCtrl();
            var itemDb = new ItemDb();
            var itemo = itemDb.GetItem(item.Id);
            ModelLibrary.Item newItem = null;
            if (item != null)
            {
                newItem = new ModelLibrary.Item
                {
                    Description = itemo.description,
                    Name = itemo.name,
                    Id = itemo.id,
                    ItemCat = itemCtrl.ConvertItemCatToModel(itemo.ItemCat),
                    Menu = menuCtrl.ConvertMenuToModel(itemo.Menu),
                    Price = priceCtrl.ConvertPriceToModel(itemo.Prices.Last())

                };
            }

            return newItem;
        }

        public IEnumerable<ModelLibrary.Item> GetMenuItems(int menuId)
        {
            var itemDb = new ItemDb();
            var priceCtrl = new PriceCtrl();

            var itemsDb = itemDb.GetMenuItems(menuId);
            var items = new List<ModelLibrary.Item>();

            foreach (var item in itemsDb)
            {
                var itemi = ConvertItemToModel(item);
                itemi.Price = priceCtrl.GetPriceItemId(item.id);
                if(itemi.Price != null)
                    items.Add(itemi);
            }

            return items;
        }
       

        public ModelLibrary.Price GetItemPrice(DatabaseAccessLibrary.Item item)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            PriceCtrl priceCtrl = new PriceCtrl();
            var prices = db.Prices.Where(p => p.itemId == item.id).OrderByDescending(p => p.startDate);
            var price = prices.First();

            ModelLibrary.Price varPrice = new ModelLibrary.Price
            {
                StartDate = price.startDate,
                EndDate = price.endDate,
                VarPrice = price.price1
            };

            return varPrice;
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

        public DatabaseAccessLibrary.Item CreateItem(ModelLibrary.Item item)
        {
            var itemDb = new ItemDb();
            var returnItem = new DatabaseAccessLibrary.Item
            {
                
                name = item.Name,
                menuId = item.Menu.Id,
                description = item.Description,

                
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
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            PriceCtrl priceCtrl = new PriceCtrl();
            MenuCtrl menuCtrl = new MenuCtrl();

            var modelItem = new ModelLibrary.Item
            {
                Id = dbItem.id,
                Name = dbItem.name,
                Description = dbItem.description,
                
                

            };
            return modelItem;
        }

        public DatabaseAccessLibrary.Item ConvertItemToDb(ModelLibrary.Item modelItem)// , int menuId menuId = menuId 
        {
            if (modelItem == null)
                return null;
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            PriceCtrl priceCtrl = new PriceCtrl();
            //var CheckItem = db.Items.Single(a => a.id == modelItem.Id);
            //var prices = db.Prices.Where(p => p.itemId == modelItem.Id).OrderByDescending(p => p.startDate);
            //var price = prices.First();
            var menuCtrl = new MenuCtrl();
           
            var dbItem = new DatabaseAccessLibrary.Item
            {
                menuId = modelItem.Menu.Id,
                itemCatId = modelItem.ItemCat.Id,
                id = modelItem.Id,
                name = modelItem.Name,
                description = modelItem.Description,
                ItemCat = ConvertItemCatToDb(modelItem.ItemCat),
                Menu = menuCtrl.ConvertMenuToDb(modelItem.Menu)
                
                
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
            if (dbItemCat == null)
                return null;

            var modelItemCat = new ModelLibrary.ItemCat
            {
                Id = dbItemCat.id,
                Name = dbItemCat.name
            };
            

            return modelItemCat;
        }

        public DatabaseAccessLibrary.ItemCat ConvertItemCatToDb(ModelLibrary.ItemCat modelItemCat)
        {
            if (modelItemCat == null)
                return null;
            var itemCat = new DatabaseAccessLibrary.ItemCat();
            itemCat.id = modelItemCat.Id;
            itemCat.name = modelItemCat.Name;
            
            return itemCat;

        }

        public void DeleteItem(ModelLibrary.Item item)
        {
            var itemDb = new ItemDb();
            var dbItem = ConvertItemToDb(item);
            itemDb.DeleteItem(dbItem);
        }

        public void UpdateItem(ModelLibrary.Item beforeItem, ModelLibrary.Item afterItem)
        {
            var itemDb = new ItemDb();

            var beforeDbItem = ConvertItemToDb(beforeItem);
            var afterDbItem = ConvertItemToDb(afterItem);
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
