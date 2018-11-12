using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class ItemDb
    {
        public void AddItem(Item item)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            if (db.Items.Any(t => !(t.name == item.name && t.id == item.id
            && )))
                db.Items.InsertOnSubmit(item);
            db.SubmitChanges();
        }

        public Item GetItem(int id, string name, Price price )
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var dbItem = db.Items.Single(a => a.id == id && a.name == name && a.Prices.g== price)); //Price????
            return dbItem;
        }

        public void DeleteItem(Item item)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            
            if (db.Items.Any(t => !(t.name == item.name && t.id == item.id)))
                db.Items.DeleteOnSubmit(item);
            db.SubmitChanges();
        }

        public void UpdateItem(Item beforeItem, Item afterItem)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var newItem = db.Items.SingleOrDefault(a => a.name == beforeItem.name &&
                          a.description == beforeItem.description && a.Prices.Equals(beforeItem.Prices)); //Price????
            newItem = afterItem;
            db.SubmitChanges();
        }

        public IEnumerable<Item> GetItems()
        {
            var db = new JustFeastDbDataContext();

            var items = db.Items.AsEnumerable();
            return items;
        }


    }
}
