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

            if (db.Items.Any(t => !(t.name == item.name && t.id == item.id )))
                db.Items.InsertOnSubmit(item);
            

        }

        public DatabaseAccessLibrary.Item GetItem(int id, int menuId, string name, string description, int itemCatId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var dbItem = db.Items.Single(a => a.id == id && a.menuId == menuId && a.name == name && a.description == description
                                              && a.itemCatId == itemCatId);
            return dbItem;
        }

        public void DeleteItem(Item item)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            if (db.Items.Any(t => !(t.name == item.name && t.id == item.id)))
                db.Items.DeleteOnSubmit(item);

        }
    }
}
