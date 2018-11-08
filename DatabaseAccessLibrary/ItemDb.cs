using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class ItemDb
    {
        public void AddItem(Item item, Menu menu, Restaurant restaurant)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            if (db.Items.Any(t => !(t.name == item.name && t.id == item.id )))
                db.Items.InsertOnSubmit(item);

        }

        public void GetItem()
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            if (db.Items.FirstOrDefault <Item>(name))
                { }
        }
    }
}
