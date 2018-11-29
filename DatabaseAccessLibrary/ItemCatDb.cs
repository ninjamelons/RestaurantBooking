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
    

        public  ItemCat GetItemCat(string name, int itemCatId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var itemCat = db.ItemCats.Single(t => t.name == name && t.id == itemCatId);
            return itemCat;
        }

        public  void DeleteItemCat(ItemCat itemCat)
        {
            var db = new JustFeastDbDataContext();
            //DatabaseAccessLibrary.ItemCat dbItemCat = db.ItemCats.First(t => t.name == itemCat.name);
                                                        
            //if (dbItemCat != null)
           // {
                db.Items.DeleteOnSubmit(db.Items.First(t => t.id == itemCat.id && t.name ==  itemCat.name));
                db.SubmitChanges();
           // }
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
