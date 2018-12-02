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
        public ModelLibrary.Menu CreateMenu(ModelLibrary.Menu menu)
        {
            var dbMenu = new DatabaseAccessLibrary.Menu();
            ItemCtrl itemCtrl = new ItemCtrl();

            dbMenu.name = menu.Name;
            dbMenu.restaurantId = menu.RestaurantId;
            dbMenu.active = menu.Active;
            DatabaseAccessLibrary.MenuDb menuDb = new DatabaseAccessLibrary.MenuDb();
            menuDb.AddMenu(dbMenu);
            return menu;


        }
        public void DleteMenu(int menuId)
        {
            var menuDb = new MenuDb();
            menuDb.DeleteMenu(menuId);
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
            if(itemList.Count > 0)
            {
                modelMenu.RestaurantId = dbMenu.restaurantId;
                modelMenu.Id = dbMenu.id;
                modelMenu.Name = dbMenu.name;
                modelMenu.Items = itemList;
                modelMenu.Active = dbMenu.active;

                return modelMenu;
            }
            modelMenu.RestaurantId = dbMenu.restaurantId;
            modelMenu.Id = dbMenu.id;
            modelMenu.Name = dbMenu.name;
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

            var dbMenu = new DatabaseAccessLibrary.Menu
            {
                id = menu.Id,
                active = menu.Active,
                name = menu.Name,
                restaurantId = menu.RestaurantId
            };
            return dbMenu;

        }
        public void UpdateMenu(ModelLibrary.Menu beforeMenu, ModelLibrary.Menu afterMenu)
        {
            var menuDb = new MenuDb();

            var beforeDbMenu = ConvertMenuToDb(beforeMenu);
            var afterDbMenu = ConvertMenuToDb(afterMenu);
            menuDb.UpdateMenu(beforeDbMenu, afterDbMenu);

        }

        public ModelLibrary.Menu GetMenuById(int menuId)
        {
            var menuDb = new MenuDb();
            var menu = menuDb.GetMenu(menuId);
            ModelLibrary.Menu newMenu = null;
            var modelMenu = ConvertMenuToModel(menu);
            if (menu != null)
            {
                return modelMenu;
            }
            return newMenu;

        }

        public ModelLibrary.Menu GetMenuByName(string name)
        {
            var menuDb = new MenuDb();
            var menu = menuDb.GetMenuByName(name);
            ModelLibrary.Menu newMenu = null;
            var modelMenu = ConvertMenuToModel(menu);
            if (menu != null)
            {
                return modelMenu;
            }
            return newMenu;

        }
    }
}

