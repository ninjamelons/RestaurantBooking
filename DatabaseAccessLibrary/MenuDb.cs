﻿using System;
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

        public void DeleteMenu(DatabaseAccessLibrary.Menu menu)
        {
            var db = new JustFeastDbDataContext();
            DatabaseAccessLibrary.Menu dbMenu = db.Menus.First(t => t.name == menu.name
                                                        && t.id == menu.id);
            if (dbMenu != null)
            {
                db.Menus.DeleteOnSubmit(db.Menus.First(t => t.name == menu.name
                                                        && t.id == menu.id));
                db.SubmitChanges();
            }

          
        }

        public Menu GetActiveMenu(int restaurantId)
        {
            var db = new JustFeastDbDataContext();
            var menu = db.Menus.FirstOrDefault(t => t.restaurantId == restaurantId
                                                     && t.active);
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
            var dbMenu = db.Menus.SingleOrDefault(t => t.id == beforeMenu.id
                                                  && t.restaurantId == beforeMenu.restaurantId && beforeMenu.id == afterMenu.id);

                dbMenu.restaurantId = afterMenu.restaurantId;
                dbMenu.name = afterMenu.name;
                dbMenu.Items = afterMenu.Items;
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
