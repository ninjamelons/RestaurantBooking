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
        public void UpdateItem (ModelLibrary.Item oldItem, ModelLibrary.Item newItem)
        {
                var tableDb = new TableDb();
                var oldDbItem = ConvertItemToDb(oldItem);
                var newDbItem = ConvertItemToDb(newItem);

                ItemDb.UpdateItem(oldDbItem, newDbItem);
            
        }
        public static IEnumerable<ModelLibrary.Item> GetItems()
        {
            var itemDb = new ItemDb();
            var items = itemDb.GetItems();
            var modelItems = new List<ModelLibrary.Item>();
            JustFeastDbDataContext db = new JustFeastDbDataContext();
          
            foreach (var item in items)
            {
                var CheckItem = db.Items.Single(a => a.id == item.id);
                var prices = db.Prices.Where(p => p.itemId == item.id).OrderByDescending(p => p.startDate);
                var price = prices.First();
                modelItems.Add(new ModelLibrary.Item
                {
                    Name = item.name,
                    Description = item.description,
                    ItemCatId = Convert.ToInt32(item.itemCatId),
                    Id = item.id,
                    MenuId = item.menuId,
                    Price = price.price1
                });
            }

            return modelItems;
        }
        public static ModelLibrary.Item GetItem(ModelLibrary.Item item)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var CheckItem = db.Items.Single(a => a.id == item.Id);
            var prices = db.Prices.Where(p => p.itemId == item.Id).OrderByDescending(p => p.startDate);
            var price = prices.First();


            ModelLibrary.Item returnItem = null;
            if(CheckItem != null)
            {
                returnItem = new ModelLibrary.Item
                {
                    Name = CheckItem.name,
                    Description = CheckItem.description,
                    ItemCatId = Convert.ToInt32(CheckItem.itemCatId),
                    MenuId = CheckItem.menuId,
                    Id = CheckItem.id,
                    Price = price.price1
                    
                };
            }
            return returnItem;
        }

        public static void InsertModelItemToDb(ModelLibrary.Item item)
        {

            JustFeastDbDataContext db = new JustFeastDbDataContext();
            
            var dbItem = new DatabaseAccessLibrary.Item
            {
                name = item.Name,
                description = item.Description,
                menuId = item.MenuId,
                itemCatId = item.ItemCatId
            };
            db.Items.InsertOnSubmit(dbItem);
            db.SubmitChanges();
           

        }
        public static ModelLibrary.Item CreateItem(ModelLibrary.Item modelItem,string name, string description, int menuId, int itemCatId,
                                                    DateTime startDate, DateTime endDate, double price)
        {
            
            var item = new ModelLibrary.Item
            {
                Name = name,
                Description = description,
                MenuId = menuId,
                ItemCatId = itemCatId
            };

            var context = new ValidationContext(item, null, null);
            var result = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(item, context, result, true);
            if (!isModelStateValid)
                throw new ValidationException();

            InsertModelItemToDb(item);
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            
            var newItem = GetItem(item);
            var pric = new ModelLibrary.Price
            {
                ItemId = newItem.Id,
                StartDate = startDate,
                EndDate = endDate,
                VarPrice = price
            };
            return item;
        }

        public static ModelLibrary.Item ConvertItemToModel(DatabaseAccessLibrary.Item dbItem)
        {
            var modelItem = new ModelLibrary.Item
            {
                Name = dbItem.name,
                Description = dbItem.description,
                MenuId = dbItem.menuId,
                ItemCatId = Convert.ToInt32(dbItem.itemCatId)
                
                
            };
            return modelItem;
        }

        public static DatabaseAccessLibrary.Item ConvertItemToDb(ModelLibrary.Item modelItem)
        {
            var dbItem = new DatabaseAccessLibrary.Item
            {
                name = modelItem.Name,
                description = modelItem.Description,
                menuId = modelItem.MenuId,
                itemCatId = modelItem.ItemCatId
            };
            return dbItem;
        }

        public static ModelLibrary.ItemCat CreateItemCat(string name)
        {
            var itemCat = new ModelLibrary.ItemCat
            {
                Name = name,

            };
            return itemCat;
        }
        public static ModelLibrary.ItemCat ConvertItemCatToModel(DatabaseAccessLibrary.ItemCat dbItemCat)
        {
            var modelItemCat = new ModelLibrary.ItemCat
            {
                Name = dbItemCat.name
            };
            return modelItemCat; 
        }

        public static DatabaseAccessLibrary.ItemCat ConvertItemCatToDb(ModelLibrary.ItemCat modelItemCat)
        {
            var dbItemCat = new DatabaseAccessLibrary.ItemCat
            {
                name = modelItemCat.Name
            };
            return dbItemCat;
        }

        public static void DeleteTable(ModelLibrary.Item item)
        {
            var dbItem = ConvertItemToDb(item);
            ItemDb.DeleteItem(dbItem);
        }



    }
}
