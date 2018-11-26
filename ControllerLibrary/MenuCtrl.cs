using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAccessLibrary;

namespace ControllerLibrary
{
    public class MenuCtrl
    {
        public static ModelLibrary.Menu CreateMenu(string name, int restaurantId, bool active)
        {
            var modelMenu = new ModelLibrary.Menu
            {
                Name = name,
                RestaurantId = restaurantId,
                Active = false

            };
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

        public static ModelLibrary.Menu ConvertMenuToModel(DatabaseAccessLibrary.Menu dbMenu)
        {
            var modelMenu = new ModelLibrary.Menu
            {
                Id = dbMenu.id,
                Name = dbMenu.name,
                RestaurantId = dbMenu.restaurantId,
                Active = dbMenu.active

            };
            return modelMenu;
        }

        public static DatabaseAccessLibrary.Menu ConvertMenuToDb(ModelLibrary.Menu modelMenu)
        {
            var dbMenu = new DatabaseAccessLibrary.Menu
            {
                name = modelMenu.Name,
                restaurantId = modelMenu.RestaurantId,
                active = modelMenu.Active
            };
            return dbMenu;
        }
    }
}
