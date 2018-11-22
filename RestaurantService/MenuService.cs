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
        public void CreateMenu(ModelLibrary.Menu menu)
        {
            var dbMenu = MenuCtrl.ConvertMenuToDb(menu);
            MenuDb.AddMenu(dbMenu);

        }

        public void DeleteMenu(ModelLibrary.Menu menu)
        {
            var dbMenu = MenuCtrl.ConvertMenuToDb(menu);
            MenuDb.DeleteMenu(dbMenu);
        }

        public IEnumerable<ModelLibrary.Menu> GetAllMenusByRestaurant(int restaurantId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var menus = db.Menus.Where(a => a.restaurantId == restaurantId).ToList();
            List<ModelLibrary.Menu> modelMenu = new List<ModelLibrary.Menu>();
            foreach (var a in menus)
            {
                modelMenu.Add(MenuCtrl.ConvertMenuToModel(a));
            }
            return modelMenu; ;
        }

        public void UpdateMenu(ModelLibrary.Menu menu)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var aMenu = db.Menus.SingleOrDefault(a => a.id == menu.Id);
            aMenu = MenuCtrl.ConvertMenuToDb(menu);
            db.SubmitChanges();
        }
    }
}
