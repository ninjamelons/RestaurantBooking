using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class MenuDb
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
            var db = new JustFeastDbDataContext();

            Menu dbMenu = null;
            dbMenu = db.Menus.SingleOrDefault(t => t.id == id);
                                               
            return dbMenu;
        }

        public Menu GetMenuByName(string name)
        {
            var db = new JustFeastDbDataContext();

            Menu dbMenu = null;
            dbMenu = db.Menus.First(t => t.name == name);

            return dbMenu;
        }

        public void DeleteMenu(int menuId)
        {
            var db = new JustFeastDbDataContext();
            DatabaseAccessLibrary.Menu dbMenu = db.Menus.First(t => t.id == menuId);
            if (dbMenu != null)
            {
                db.Menus.DeleteOnSubmit(db.Menus.First(t => t.id == menuId));
                db.SubmitChanges();
            }

          
        }

        public Menu GetActiveMenu(int restaurantId)
        {
            var db = new JustFeastDbDataContext();
            var menu = db.Menus.FirstOrDefault(t => t.restaurantId == restaurantId
                                                     && t.active == true);
            return menu;
        }

        public int GetActiveMenuId(int restaurantId)
        {
            var db = new JustFeastDbDataContext();
            return db.Menus.SingleOrDefault(t => t.restaurantId == restaurantId
                                                 && t.active).id;
        }

        public void UpdateMenu(Menu beforeMenu, Menu afterMenu)
        {
            var db = new JustFeastDbDataContext();
            var dbMenu = db.Menus.SingleOrDefault(t => t.id == beforeMenu.id && t.id == afterMenu.id);

                dbMenu.restaurantId = afterMenu.restaurantId;
                dbMenu.name = afterMenu.name;
                dbMenu.Items = afterMenu.Items;
                dbMenu.active = afterMenu.active;
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
