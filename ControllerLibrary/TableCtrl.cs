﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAccessLibrary;
using ModelLibrary;

namespace ControllerLibrary
{
    public class TableCtrl
    {
        public ResTable ConvertTable(Table table)
        {
            var returnTable = new ResTable();

            returnTable.noSeats = Convert.ToInt32(table.NoSeats);
            returnTable.reserved = Convert.ToInt32(table.Reserved);
            returnTable.total = Convert.ToInt32(table.Total);
            returnTable.restaurantId = Convert.ToInt32(table.RestaurantId);

            return returnTable;
        }

        public Table GetTable(Table table)
        {
            var tblDb = new TableDb();
            var tbl = tblDb.GetTable(Convert.ToInt32(table.NoSeats), Convert.ToInt32(table.RestaurantId));
            Table newTable = null;
            if (tbl != null)
            {
                newTable = new Table
                {
                    NoSeats = tbl.noSeats.ToString(),
                    Reserved = tbl.reserved.ToString(),
                    RestaurantId = tbl.restaurantId.ToString(),
                    Total = tbl.total.ToString()
                };
            }

            return newTable;
        }

        public IEnumerable<Table> GetTables()
        {
            var tblCtrl = new TableDb();
            var tables = tblCtrl.GetTables();
            var modelTables = new List<Table>();

            foreach (var table in tables)
            {
                modelTables.Add(new Table
                {
                    NoSeats = table.noSeats.ToString(),
                    Reserved = table.reserved.ToString(),
                    RestaurantId = table.restaurantId.ToString(),
                    Total = table.total.ToString()
                });
            }

            return modelTables;
        }

        public void DeleteTable(Table table)
        {
            var tableDb = new TableDb();
            var resTable = ConvertTable(table);
            tableDb.DeleteTable(resTable);
        }
    }
}