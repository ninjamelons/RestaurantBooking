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


            var item = db.Items.SingleOrDefault(t => t.id == id);
            if (item != null)
            {
                return item;
            }
            return null;
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
            if(item != null)
            return item;
            else
            {
                return null;
            }
        }
        public Item GetItemByNameAndMenu(string name, int menuId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var item = db.Items.Single(t => t.name == name && t.menuId == menuId);
            if (item != null)
                return item;
            else
            {
                return null;
            }
        }

        public  void DeleteItem(int itemId)
        {
            var db = new JustFeastDbDataContext();
            DatabaseAccessLibrary.Item dbItem = db.Items.First(t => t.id == itemId);
            if (dbItem != null)
            {
                db.Items.DeleteOnSubmit(db.Items.First(t => t.id == itemId));
                db.SubmitChanges();
            }
        }

        public void UpdateItem(Item updatedItem)
        {
            var db = new JustFeastDbDataContext();
            var dbItem = db.Items.First(t => t.id == updatedItem.id);

            dbItem.menuId = updatedItem.menuId;
            dbItem.name = updatedItem.name;
            dbItem.description = updatedItem.description;
            dbItem.itemCatId = updatedItem.itemCatId;

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

        public IEnumerable<Item> GetCategoryItems(int itemCatId)
        {
            var db = new JustFeastDbDataContext();
            return db.Items.Where(t => t.itemCatId == itemCatId).AsEnumerable().ToList();
        }
    }
}
