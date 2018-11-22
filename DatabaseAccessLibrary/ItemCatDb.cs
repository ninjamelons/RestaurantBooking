using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class ItemCatDb
    {
        public static void AddItemCat(ItemCat itemCat)
        {

            JustFeastDbDataContext db = new JustFeastDbDataContext();
            if (db.ItemCats.Any(t => !(t.name == itemCat.name)))
                db.ItemCats.InsertOnSubmit(itemCat);
            db.SubmitChanges();
        }

        public static ItemCat GetItemCat(string name)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var itemCat = db.ItemCats.Single(t => t.name == name);
            return itemCat;
        }

        public static void DeleteItemCat(ItemCat itemCat)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            if (db.ItemCats.Any(t => t.id == itemCat.id))
                db.ItemCats.DeleteOnSubmit(itemCat);
            db.SubmitChanges();
        }

        public static void UpdateItemCat(ItemCat beforeItemCat, ItemCat afterItemCat)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var itemCat = db.ItemCats.SingleOrDefault(t => t.id == beforeItemCat.id);
            itemCat = afterItemCat;
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
