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
        public void CreateMenu(ModelLibrary.Menu menu, int restaurantId)
        {
            var menuCtrl = new MenuCtrl();
            menuCtrl.CreateMenu(menu, restaurantId);
        }
        public void DeleteMenu(ModelLibrary.Menu menu, int restaurantId)
        {
            var menuCtrl = new MenuCtrl();
            var menuDb = new MenuDb();
            var dbMenu = menuCtrl.ConvertMenuToDb(menu, restaurantId);
            menuDb.DeleteMenu(dbMenu);
        }

        public IEnumerable<ModelLibrary.Menu> GetAllMenusByRestaurant(int restaurantId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            MenuCtrl menuCtrl = new MenuCtrl();
            var res = db.Menus.Where(x => x.restaurantId == restaurantId).ToList();
            List<ModelLibrary.Menu> modelMenu = new List<ModelLibrary.Menu>();
            foreach (var x in res)
            {
                modelMenu.Add(menuCtrl.ConvertMenuToModel(x));
            }
            return modelMenu;
           
        }

        public void UpdateMenu(ModelLibrary.Menu beforeMenu, ModelLibrary.Menu afterMenu, int restaurantId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            MenuCtrl menuCtrl = new MenuCtrl();
            menuCtrl.UpdateMenu(beforeMenu, afterMenu, restaurantId);
        }
    }
}
