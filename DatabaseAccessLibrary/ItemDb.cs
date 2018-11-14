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

                db.Items.InsertOnSubmit(item);
            db.SubmitChanges();
        }

        public Item GetItem(int id, string name)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var item = db.Items.Single(t => t.name == name && t.id == id);
            return item;
        }

        public void DeleteItem(Item item)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            if (db.Items.Any(t => t.id == item.id))
                db.Items.DeleteOnSubmit(item);
            db.SubmitChanges();
        }

        public void UpdateItem(Item beforeItem, Item afterItem)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var item = db.Items.SingleOrDefault(t => t.id == beforeItem.id);
            item = afterItem;
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
