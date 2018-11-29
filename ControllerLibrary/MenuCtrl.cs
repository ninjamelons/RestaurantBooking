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
        public DatabaseAccessLibrary.Menu CreateMenu(ModelLibrary.Menu menu)
        {
            var returnMenu = new DatabaseAccessLibrary.Menu();
            ItemCtrl itemCtrl = new ItemCtrl();

            
            returnMenu.name = menu.Name;
            returnMenu.restaurantId = menu.RestaurantId;
            returnMenu.Items = null;
            DatabaseAccessLibrary.MenuDb menuDb = new DatabaseAccessLibrary.MenuDb();
            menuDb.AddMenu(returnMenu);
            return returnMenu;
            

        }

        public ModelLibrary.Menu ConvertMenuToModel(DatabaseAccessLibrary.Menu dbMenu)
        {
            var modelMenu = new ModelLibrary.Menu();
            List<ModelLibrary.Item> itemList = new List<ModelLibrary.Item>();
            ItemCtrl itemCtrl = new ItemCtrl();
            if (dbMenu == null)
                return modelMenu;

            foreach (var Item in dbMenu.Items)
            {
                itemList.Add(itemCtrl.ConvertItemToModel(Item));
            }

            modelMenu.RestaurantId = dbMenu.restaurantId;
            modelMenu.Id = dbMenu.id;
            modelMenu.Name = dbMenu.name;
            modelMenu.Items = itemList;
            modelMenu.Active = dbMenu.active;

      
            return modelMenu;
        }

        public ModelLibrary.Menu GetActiveMenu(int restaurantId)
        {
            var menuDb = new MenuDb();
            var itemCtrl = new ItemCtrl();

            var menu = ConvertMenuToModel(menuDb.GetActiveMenu(restaurantId));
            menu.Items = itemCtrl.GetMenuItems(menu.Id);
            return menu;
        }
        

    

        public DatabaseAccessLibrary.Menu ConvertMenuToDb(ModelLibrary.Menu menu)
        {

            if (menu == null)
                return null;
            var returnMenu = new DatabaseAccessLibrary.Menu();
            ItemCtrl itemCtrl = new ItemCtrl();

            returnMenu.restaurantId = menu.RestaurantId;
            returnMenu.id = menu.Id;
            returnMenu.name = menu.Name;
            //returnMenu.restaurantId = restaurantId;
            foreach (var Item in menu.Items)
            {
                returnMenu.Items.Add(itemCtrl.ConvertItemToDb(Item));
            }
            DatabaseAccessLibrary.MenuDb menuDb = new DatabaseAccessLibrary.MenuDb();
            return returnMenu;
            
        }
        public void UpdateMenu(ModelLibrary.Menu beforeMenu, ModelLibrary.Menu afterMenu)
        {
            var menuDb = new MenuDb();

            var beforeDbMenu = ConvertMenuToDb(beforeMenu);
            var afterDbMenu = ConvertMenuToDb(afterMenu);
            menuDb.UpdateMenu(beforeDbMenu, afterDbMenu);

        }
        public ModelLibrary.Menu GetMenu(ModelLibrary.Menu menuModel)
        {
            var menuDb = new MenuDb();
            var itemCtrl = new ItemCtrl();
            var menu = menuDb.GetMenu(menuModel.Id);
            ModelLibrary.Menu newMenu = null;
            var modelMenu = ConvertMenuToModel(menu);
            if (menu != null)
            {
                newMenu = new ModelLibrary.Menu
                {
                    RestaurantId = menu.restaurantId,
                    Name = menu.name,
                    Active = menu.active,
                    Items = modelMenu.Items.AsEnumerable()
  
                };
            }

            return newMenu;
        }

    }
}
