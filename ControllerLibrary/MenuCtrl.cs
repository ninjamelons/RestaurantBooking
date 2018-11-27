using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using DatabaseAccessLibrary;

namespace ControllerLibrary
{
    public class MenuCtrl
    {
        public DatabaseAccessLibrary.Menu CreateMenu(ModelLibrary.Menu menu, int restaurantId)
        {
            var returnMenu = new DatabaseAccessLibrary.Menu();
            ItemCtrl itemCtrl = new ItemCtrl();

            
            returnMenu.name = menu.Name;
            returnMenu.restaurantId = restaurantId;
            foreach(var Item in menu.Items)
            {
                returnMenu.Items.Add(itemCtrl.ConvertItemToDb(Item, returnMenu.id)); //64
            }
            DatabaseAccessLibrary.MenuDb menuDb = new DatabaseAccessLibrary.MenuDb();
            menuDb.AddMenu(returnMenu);
            return returnMenu;
        }

        public ModelLibrary.Menu GetActiveMenu(int restaurantId)
        {
            var menuDb = new MenuDb();
            var itemCtrl = new ItemCtrl();

            var menu = ConvertMenuToModel(menuDb.GetActiveMenu(restaurantId));
            menu.Items = itemCtrl.GetMenuItems(menu.Id);
            return menu;
        }

        public ModelLibrary.Menu ConvertMenuToModel(DatabaseAccessLibrary.Menu dbMenu)
        {
            var modelMenu = new ModelLibrary.Menu();
            List<ModelLibrary.Item> itemList = new List<ModelLibrary.Item>();
            ItemCtrl itemCtrl = new ItemCtrl();

            foreach (var Item in dbMenu.Items)
            {
                itemList.Add(itemCtrl.ConvertItemToModel(Item));
            }
            modelMenu.Id = dbMenu.id;
            modelMenu.Name = dbMenu.name;
            modelMenu.Items = itemList;
            modelMenu.Active = dbMenu.active;

      
            return modelMenu;
        }

        public DatabaseAccessLibrary.Menu ConvertMenuToDb(ModelLibrary.Menu menu, int restaurantId)
        {

            if (menu == null)
                return null;
            var returnMenu = new DatabaseAccessLibrary.Menu();
            ItemCtrl itemCtrl = new ItemCtrl();

            returnMenu.id = Convert.ToInt32(menu.Id);
            returnMenu.name = menu.Name;
            returnMenu.restaurantId = restaurantId;
            foreach (var Item in menu.Items)
            {
                returnMenu.Items.Add(itemCtrl.ConvertItemToDb(Item, returnMenu.id));
            }
            DatabaseAccessLibrary.MenuDb menuDb = new DatabaseAccessLibrary.MenuDb();
            return returnMenu;
            
        }
        public void UpdateMenu(ModelLibrary.Menu beforeMenu, ModelLibrary.Menu afterMenu, int restaurantId)
        {
            var menuDb = new MenuDb();

            var beforeDbMenu = ConvertMenuToDb(beforeMenu, restaurantId);
            var afterDbMenu = ConvertMenuToDb(afterMenu, restaurantId);
            menuDb.UpdateMenu(beforeDbMenu, afterDbMenu);

        }
        
    }
}
