using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class ItemCatDb
    {
        public  void AddItemCat(ItemCat itemCat)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            db.ItemCats.InsertOnSubmit(itemCat);
            db.SubmitChanges();
        }

        public ItemCat GetItemCatById(int id)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var item = db.ItemCats.Single(t => t.id == id);
            return item;

        }

        public ItemCat GetItemCatByItemCatId(int itemId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var item = db.Items.Where(t => t.id == itemId).SingleOrDefault();
            var itemCat = db.ItemCats.Where(t => t.id == item.itemCatId).SingleOrDefault();

            return itemCat;
            
            
        }
        public ItemCat GetITemCatByName(string name)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var item = db.ItemCats.First(t => t.name == name);
            return item;
        }


        public  void DeleteItemCat(int itemCatId)
        {
            var db = new JustFeastDbDataContext();
            var check = db.ItemCats.SingleOrDefault(t => t.id == itemCatId);
            if (check != null)
            {
                db.ItemCats.DeleteOnSubmit(db.ItemCats.First(t => t.id == itemCatId));
                db.SubmitChanges();
            }
           
        }

        public  void UpdateItemCat(ItemCat beforeItemCat, ItemCat afterItemCat)
        {
            var db = new JustFeastDbDataContext();
            var dbItemCat = db.ItemCats.SingleOrDefault(t => t.id == beforeItemCat.id && beforeItemCat.id == afterItemCat.id);

            dbItemCat.id = afterItemCat.id;
            dbItemCat.name = afterItemCat.name;

           
            db.SubmitChanges();
        }

        public IEnumerable<ItemCat> GetItemCats()
        {
            var db = new JustFeastDbDataContext();

            var itemCats = db.ItemCats.AsEnumerable();
            return itemCats;
        }

    }
}
