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
            db.SubmitChanges();
            
            

        }

        public DatabaseAccessLibrary.Item GetItem(int id, string name)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var dbItem = db.Items.Single(a => a.id == id && a.name == name); 
            return dbItem;
        }

        public void DeleteItem(Item item)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            
            if (db.Items.Any(t => !(t.name == item.name && t.id == item.id)))
                db.Items.DeleteOnSubmit(item);
            db.SubmitChanges();
        }

        public void UpdateItem(Item item)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var dbItem = db.Items


        }
    }
}
