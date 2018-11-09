﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class TableDb
    {
        public void AddTable(ResTable resTable)
        {
            var db = new JustFeastDbDataContext();

            if(db.ResTables.Any(t => !(t.noSeats == resTable.noSeats 
                                       && t.restaurantId == resTable.restaurantId)))
                db.ResTables.InsertOnSubmit(resTable);
            db.SubmitChanges();
            
        }
        public ResTable GetTable(int noSeats, int restaurantId)
        {
            var db = new JustFeastDbDataContext();

            var resTable = db.ResTables.Single(t => t.noSeats == noSeats
                                                    && t.restaurantId == restaurantId);
            return resTable;
        }
        public IEnumerable<ResTable> GetTables()
        {
            var db = new JustFeastDbDataContext();

            var resTables = db.ResTables.AsEnumerable();
            return resTables;
        }
        public void UpdateTable(ResTable oldTable, ResTable newTable)
        {
            var db = new JustFeastDbDataContext();
            var resTable = db.ResTables.SingleOrDefault(t => t.noSeats == oldTable.noSeats
                                              && t.restaurantId == oldTable.restaurantId);
            resTable = newTable;
            db.SubmitChanges();

        }
        public void DeleteTable(ResTable resTable)
        {
            var db = new JustFeastDbDataContext();

            if(db.ResTables.Any(t => t.noSeats == resTable.noSeats 
                                     && t.restaurantId == resTable.restaurantId))
                db.ResTables.DeleteOnSubmit(resTable);
            db.SubmitChanges();
        }
    }
}