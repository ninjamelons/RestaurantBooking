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

        public IEnumerable<ModelLibrary.Item> GetCatItems(int itemCatId)
        {
            var itemDb = new ItemDb();
            var priceCtrl = new PriceCtrl();

            var itemsDb = itemDb.GetCategoryItems(itemCatId);
            var items = new List<ModelLibrary.Item>();

            foreach (var item in itemsDb)
            {
                var itemo = ConvertItemToModel(item);
                itemo.Price = priceCtrl.GetLatestPriceById(item.id);
                if (itemo.Price != null)
                    items.Add(itemo);
            }

            return items;
        }
       

        public ModelLibrary.Price GetItemPrice(int itemId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            PriceCtrl priceCtrl = new PriceCtrl();
            var prices = db.Prices.Where(p => p.itemId == itemId).OrderByDescending(p => p.startDate);
            var price = prices.First();
            if(price !=null)
            {
                ModelLibrary.Price varPrice = new ModelLibrary.Price
                {
                    StartDate = price.startDate,
                    EndDate = price.endDate,
                    VarPrice = price.price1
                };

                return varPrice;
            }
            return null;
        }
        public ModelLibrary.Item GetItem(int itemId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var itemDb = new ItemDb();
            PriceCtrl priceCtrl = new PriceCtrl();
            return ConvertItemToModel(itemDb.GetItem(itemId));

        }
        public ModelLibrary.ItemCat GetItemCatById(int itemCatId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var itemCatDb = new ItemCatDb();
            PriceCtrl priceCtrl = new PriceCtrl();
            return ConvertItemCatToModel(itemCatDb.GetItemCatById(itemCatId));
        }
        public ModelLibrary.ItemCat GetItemCatByName(string name)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var itemCatDb = new ItemCatDb();
            PriceCtrl priceCtrl = new PriceCtrl();
            return ConvertItemCatToModel(itemCatDb.GetITemCatByName(name));
        }
        public ModelLibrary.Item CreateItem(ModelLibrary.Item item, int menuId, int itemCatId)
        {
            var itemDb = new ItemDb();
            var dbItem = new DatabaseAccessLibrary.Item
            { 
                name = item.Name,
                menuId = menuId,
                description = item.Description,
                itemCatId = itemCatId,

            };
            var modelItem = new ModelLibrary.Item
            {
                Name = item.Name,
                Description = item.Description,
                
            };

            var context = new ValidationContext(item, null, null);
            var result = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(item, context, result, true);
            if (!isModelStateValid)
                throw new ValidationException();

            itemDb.AddItem(dbItem);

            return modelItem;
        }
        public ModelLibrary.Item ConvertItemToModelWithoutPrice(DatabaseAccessLibrary.Item dbItem)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            PriceCtrl priceCtrl = new PriceCtrl();
            MenuCtrl menuCtrl = new MenuCtrl();
            
            if (dbItem != null)
            {

                var modelItem = new ModelLibrary.Item
                {
                    Id = dbItem.id,
                    Name = dbItem.name,
                    Description = dbItem.description,
                    
                };
                return modelItem;
            }
            else
            {
                var modelItem = new ModelLibrary.Item
                {
                    Id = dbItem.id,
                    Name = dbItem.name,
                    Description = dbItem.description,
                };
                return modelItem;
            }


        }


        public ModelLibrary.Item ConvertItemToModel(DatabaseAccessLibrary.Item dbItem)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            PriceCtrl priceCtrl = new PriceCtrl();
            MenuCtrl menuCtrl = new MenuCtrl();
            //var CheckItem = db.Items.Single(a => a.id == dbItem.id);
            
            DatabaseAccessLibrary.Price price = db.Prices.Where(p => p.itemId == dbItem.id).OrderByDescending(p => p.startDate).First();
            if (dbItem != null && price != null)
            {
                
                var modelItem = new ModelLibrary.Item
                {
                    Id = dbItem.id,
                    Name = dbItem.name,
                    Description = dbItem.description,
                    Price =  priceCtrl.ConvertPriceToModel(price),
                };
                return modelItem;
            }
            else
            {
                var modelItem = new ModelLibrary.Item
                {
                    Id = dbItem.id,
                    Name = dbItem.name,
                    Description = dbItem.description,
                };
                return modelItem;
            }
            
            
        }

        public DatabaseAccessLibrary.Item ConvertItemToDb(ModelLibrary.Item modelItem, int menuId, int itemCatId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            PriceCtrl priceCtrl = new PriceCtrl();
            //var CheckItem = db.Items.Single(a => a.id == modelItem.Id);
            var menu = db.Menus.SingleOrDefault(p => p.id == menuId);
            var itemCat = db.ItemCats.SingleOrDefault(p => p.id == itemCatId);
            if( menu != null && itemCat != null && modelItem != null)
            {
                var dbItem = new DatabaseAccessLibrary.Item
                {
                    id = modelItem.Id,
                    description = modelItem.Description,
                    itemCatId = itemCatId,
                    menuId = menuId,
                    name = modelItem.Name,

                };
                return dbItem;
            }
            return null;
        }

        public ModelLibrary.ItemCat CreateItemCat(ModelLibrary.ItemCat itemCat)
        {
            var itemDb = new ItemDb();
            var itemCatDb = new ItemCatDb();
            
            var returnItemCat = new ModelLibrary.ItemCat
            {
                Name = itemCat.Name,
                
            };
            var dbItemCat = new DatabaseAccessLibrary.ItemCat
            {
                name = itemCat.Name
            };

            itemCatDb.AddItemCat(dbItemCat);

            return returnItemCat;
        }
        public ModelLibrary.ItemCat ConvertItemCatToModel(DatabaseAccessLibrary.ItemCat dbItemCat)
        {
            var itemDb = new ItemDb();
            var modelItemCat = new ModelLibrary.ItemCat();
            List<ModelLibrary.Item> itemList = new List<ModelLibrary.Item>();
            ItemCtrl itemCtrl = new ItemCtrl();
            if (dbItemCat == null)
                return modelItemCat;

            foreach (var Item in dbItemCat.Items)
            {
                itemList.Add(itemCtrl.ConvertItemToModel(Item));
            }
            if (itemList.Count > 0)
            {
                modelItemCat.Name = dbItemCat.name;
                modelItemCat.Id = dbItemCat.id;
                modelItemCat.Items = itemList;

                return modelItemCat;
            }
            modelItemCat.Name = dbItemCat.name;
            modelItemCat.Id = dbItemCat.id;

            return modelItemCat;

        }

        public DatabaseAccessLibrary.ItemCat ConvertItemCatToDb(ModelLibrary.ItemCat modelItemCat)
        {
            if (modelItemCat == null)
                return null;
            var itemCat = new DatabaseAccessLibrary.ItemCat
            {
                id = modelItemCat.Id,
                name = modelItemCat.Name,
            };
            
            return itemCat;

        }

        public void DeleteItem(int itemId)
        {
            var itemDb = new ItemDb();
            itemDb.DeleteItem(itemId);
        }

        public void UpdateItem(ModelLibrary.Item item, int menuId, int itemCatId)
        {
            var itemDb = new ItemDb();
            var dbItem = ConvertItemToDb(item, menuId, itemCatId); 
            itemDb.UpdateItem(dbItem);

        }
        public void DeleteItemCat(int itemCatId)
        {
            var itemCatDb = new ItemCatDb();
            itemCatDb.DeleteItemCat(itemCatId);
        }

        public void UpdateItemCat(ModelLibrary.ItemCat beforeItemCat, ModelLibrary.ItemCat afterItemCat)
        {
            var itemCatDb = new ItemCatDb();

            var beforeDbItemCat = ConvertItemCatToDb(beforeItemCat);
            var afterDbItemCat = ConvertItemCatToDb(afterItemCat);
            itemCatDb.UpdateItemCat(beforeDbItemCat, afterDbItemCat);

        }

        public ModelLibrary.ItemCat GetItemCatByItemCatId(int itemId)
        {
            ItemCatDb itemCatDb = new ItemCatDb();
            return ConvertItemCatToModel(itemCatDb.GetItemCatByItemCatId(itemId));
        }

    }
}
