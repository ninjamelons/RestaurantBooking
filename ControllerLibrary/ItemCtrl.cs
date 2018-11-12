using DatabaseAccessLibrary;
using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLibrary
{
    class ItemCtrl
    {
        public ModelLibrary.Item GetItem(ModelLibrary.Item item)
        {
            ItemDb itemdb = new ItemDb();
            var AnotherItem = itemdb.GetItem(item.Name, item.Description)

            return Item();
        }

        public IEnumerable<Item> GetItem()
        {
            return IEnumerable<Item>
        }

        public void DeleteTable()
        {

        }
    }
}
