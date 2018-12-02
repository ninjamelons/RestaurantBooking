using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ModelLibrary;
using ControllerLibrary;
using DatabaseAccessLibrary;

namespace RestaurantService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MenuService" in both code and config file together.
    public class MenuService : IMenuService
    {
        MenuCtrl menuCtrl = new MenuCtrl();
        public void CreateMenu(ModelLibrary.Menu menu)
        {
            var menuCtrl = new MenuCtrl();
            menuCtrl.CreateMenu(menu);
        }
        public void DeleteMenu(int menuId)
        {
            var menuDb = new MenuDb();
            var menuCtrl = new MenuCtrl();
            menuDb.DeleteMenu(menuId);
        }

        public IEnumerable<ModelLibrary.Menu> GetAllMenusByRestaurant(int restaurantId)
        {
            var menuCtrl = new MenuCtrl();
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var res = db.Menus.Where(x => x.restaurantId == restaurantId).ToList();
            List<ModelLibrary.Menu> modelMenu = new List<ModelLibrary.Menu>();
            foreach (var a in res)
            {
                modelMenu.Add(menuCtrl.ConvertMenuToModel(a));
            }
            return modelMenu;
           
        }

        public ModelLibrary.Menu GetMenu(int menuId)
        {
            return menuCtrl.GetMenuById(menuId);
        }

        public ModelLibrary.Menu GetMenuById(int menuId)
        {
            return menuCtrl.GetMenuById(menuId);
        }

        public ModelLibrary.Menu GetMenuByName(string name)
        {
            return menuCtrl.GetMenuByName(name);
        }

        public void UpdateMenu(ModelLibrary.Menu beforeMenu, ModelLibrary.Menu afterMenu)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            MenuCtrl menuCtrl = new MenuCtrl();
            menuCtrl.UpdateMenu(beforeMenu, afterMenu);
        }
    }
}
