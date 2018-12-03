﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class TableDb
    {
        private JustFeastDbDataContext db;

        public TableDb()
        {
            db = new JustFeastDbDataContext();
        }

        public IEnumerable<ResTable> GetRestaurantTables(int resId)
        {
            var tables = db.ResTables.Where(t => t.restaurantId == resId).AsEnumerable();
            return tables;
        }

        public IEnumerable<ResTable> GetAvailableRestaurantTables(int resId)
        {
            var tables = GetRestaurantTables(resId);

            tables = from table in tables
                where table.reserved == false
                select table;

            return tables;
        }

        /*
        public void AddTable(ResTable resTable)
        {
            var db = new JustFeastDbDataContext();

            if (db.ResTables.FirstOrDefault(t => !(t.noSeats == resTable.noSeats
                                                   && t.restaurantId == resTable.restaurantId)) == null)
            {
                db.ResTables.InsertOnSubmit(resTable);
                db.SubmitChanges();
            }
        }

        public ResTable GetTable(int noSeats, int restaurantId)
        {
            var db = new JustFeastDbDataContext();

            ResTable resTable = null;
            resTable = db.ResTables.SingleOrDefault(t => t.noSeats == noSeats
                                                && t.restaurantId == restaurantId);
            return resTable;
        }
        public IEnumerable<ResTable> GetTables()
        {
            var db = new JustFeastDbDataContext();

            var resTables = db.ResTables.AsEnumerable();
            return resTables;
        }
        public IEnumerable<ResTable> GetRestaurantTables(int restaurantId)
        {
            var allTables = GetTables();
            var resTables = from table in allTables
                where table.restaurantId == restaurantId
                select table;
            return resTables;
        }
        public void UpdateTable(ResTable oldTable, ResTable newTable)
        {
            var db = new JustFeastDbDataContext();
            var resTable = db.ResTables.SingleOrDefault(t => t.noSeats == oldTable.noSeats
                                              && t.restaurantId == oldTable.restaurantId);

            resTable.restaurantId = newTable.restaurantId;
            resTable.noSeats = newTable.noSeats;
           // resTable.total = newTable.total;
            resTable.reserved = newTable.reserved;
            db.SubmitChanges();
        }
        public void DeleteTable(ResTable resTable)
        {
            var db = new JustFeastDbDataContext();
            ResTable tableRes = db.ResTables.First(t => t.noSeats == resTable.noSeats 
                                                        && t.restaurantId == resTable.restaurantId);
            if (tableRes != null)
            {
                db.ResTables.DeleteOnSubmit(db.ResTables.First(t => t.noSeats == resTable.noSeats 
                                                        && t.restaurantId == resTable.restaurantId));
                db.SubmitChanges();
            }
        }*/
    }
}
