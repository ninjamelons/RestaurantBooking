using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class ItemDb
    {

        public static void AddItem(Item item)
        {

            JustFeastDbDataContext db = new JustFeastDbDataContext();

                db.Items.InsertOnSubmit(item);
            db.SubmitChanges();
        }

        public static Item GetItem(int id, string name)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var item = db.Items.Single(t => t.name == name && t.id == id);
            return item;
        }

        public static void DeleteItem(Item item)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            if (db.Items.Any(t => t.id == item.id))
                db.Items.DeleteOnSubmit(item);
            db.SubmitChanges();
        }

        public static void UpdateItem(Item beforeItem, Item afterItem)
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

        public IEnumerable<Item> GetMenuItems(int menuId)
        {
            var db = new JustFeastDbDataContext();
            return db.Items.Where(t => t.menuId == menuId).AsEnumerable().ToList();
        }
    }
}
