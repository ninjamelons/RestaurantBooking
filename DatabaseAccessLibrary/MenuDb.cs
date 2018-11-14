using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    class MenuDb
    {
        public void AddMenu(Menu menu)
        {

            JustFeastDbDataContext db = new JustFeastDbDataContext();

            if (db.Menus.Any(t => !(t.restaurantId == menu.restaurantId && t.active == menu.active && t.active == true)))

                db.Menus.InsertOnSubmit(menu);
            db.SubmitChanges();
        }

        public Menu GetMenu(int id)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var menu = db.Menus.Single(t => t.id == id );
            return menu;
        }

        public void DeleteMenu(Menu menu)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            if (db.Menus.Any(t => t.id == menu.id))
                db.Menus.DeleteOnSubmit(menu);
            db.SubmitChanges();
        }

        public void UpdateMenu(Menu beforeMenu, Menu afterMenu)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var menu = db.Menus.SingleOrDefault(t => t.id == beforeMenu.id);
            menu = afterMenu;
            db.SubmitChanges();
        }

        public IEnumerable<Menu> GetMenus()
        {
            var db = new JustFeastDbDataContext();

            var menus = db.Menus.AsEnumerable();
            return menus;
        }



    }
}
