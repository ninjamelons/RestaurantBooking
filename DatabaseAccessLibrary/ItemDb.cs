using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class ItemDb
    {
        public Item GetItem(int id)
        {
            var db = new JustFeastDbDataContext();

            Item item = null;
            item = db.Items.SingleOrDefault(t => t.id == id
                                                );
            return item;
        }
        public  void AddItem(Item item)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            db.Items.InsertOnSubmit(item);
            db.SubmitChanges();
        }
        public Item GetItemByName(string name)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var item = db.Items.First(t => t.name == name);
            return item;
        }
        public Item GetItemByNameAndMenu(string name, int menuId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var item = db.Items.Single(t => t.name == name && t.menuId == menuId);
            return item;
        }
        public  Item GetItem(int id, string name)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var item = db.Items.Single(t => t.name == name && t.id == id);
            return item;
        }

        public  void DeleteItem(Item item)
        {
            var db = new JustFeastDbDataContext();
            DatabaseAccessLibrary.Item dbItem = db.Items.First(t => t.name == item.name
                                                        && t.id == item.id);
            if (dbItem != null)
            {
                db.Items.DeleteOnSubmit(db.Items.First(t => t.name == item.name
                                                        && t.id == item.id));
                db.SubmitChanges();
            }
        }

        public void UpdateItem(Item beforeItem, Item afterItem)
        {
            var db = new JustFeastDbDataContext();
            var dbItem = db.Items.SingleOrDefault(t => t.id == beforeItem.id
                                                  && t.menuId == beforeItem.menuId && beforeItem.id == afterItem.id);

            dbItem.menuId = afterItem.menuId;
            dbItem.name = afterItem.name;
            dbItem.Prices = afterItem.Prices;
            dbItem.description = afterItem.description;
            dbItem.ItemCat = afterItem.ItemCat;
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
