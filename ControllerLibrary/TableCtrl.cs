using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseAccessLibrary;
using ModelLibrary;

namespace ControllerLibrary
{
    public class TableCtrl
    {
        /*private ResTable ConvertTable(Table table)
        {
            var returnTable = new ResTable();

            returnTable.noSeats = Convert.ToInt32(table.NoSeats);
            returnTable.reserved = Convert.ToInttable.Reserved);
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
            var tblDb = new TableDb();
            var tables = tblDb.GetTables();
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

        public IEnumerable<Table> GetRestaurantTables(int restaurantId)
        {
            var tableDb = new TableDb();
            var resTables = tableDb.GetRestaurantTables(restaurantId);
            
            var modelTables = new List<Table>();

            foreach (var table in resTables)
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

        public void CreateTable(Table table)
        {
            var tableDb = new TableDb();
            var resTable = ConvertTable(table);
            tableDb.AddTable(resTable);
        }

        public void UpdateTable(Table oldTable, Table newTable)
        {
            var tableDb = new TableDb();
            var oldResTable = ConvertTable(oldTable);
            var newResTable = ConvertTable(newTable);

            tableDb.UpdateTable(oldResTable, newResTable);
        }

        public void DeleteTable(Table table)
        {
            var tableDb = new TableDb();
            var resTable = ConvertTable(table);
            tableDb.DeleteTable(resTable);
        }
        */

        private Table ConvertTableToModel(ResTable table)
        {
            var modelTable = new Table();

            modelTable.NoSeats = table.noSeats;
            modelTable.Reserved = table.reserved;
            modelTable.RestaurantId = table.restaurantId;

            return modelTable;
        }

        private IEnumerable<Table> ConvertTableListToModel(IEnumerable<ResTable> tables)
        {
            List<Table> modelTables = new List<Table>();
            foreach (var table in tables)
            {
                modelTables.Add(ConvertTableToModel(table));
            }

            return modelTables;
        }

        public int ReserveTables(int resId, int noSeats, DateTime dateTime)
        {
            //Algorithm to find suitable tables
            List<Table> tables = (List<Table>)GetAvailableRestaurantTables(resId);
            int tempSeats = noSeats;
            List<Table> reserveTables = new List<Table>();

            //Repeat until remaining seats found are equal to 0
            for (int i = 0; tempSeats > 0; i++)
            {
                //First check if there is a table that matches perfectly the noSeats required
                if (noSeats == tables[i].NoSeats)
                {
                    reserveTables.Add(tables[i]);
                    tempSeats = 0;
                }
                /*noSeats is a weird number that doesn't match any table,
                 Find the largest table with the smallest modulo,
                 Decrement available restaurants to not double down,
                 Decrease tempSeats by seats being taken away
                */
                else
                {
                    //Get all modulo for tables
                    var modulo = GetAllModulo(tables, tempSeats);

                    //Get smallest modulo
                    var smallDict = FindSmallestModulo(modulo);

                    //Get tables with matching smallest modulo
                    var tablesModulo = GetTablesSmallestModulo(smallDict, tables);

                    //Get largest table
                    var largestTable = GetLargestTable(tablesModulo);

                    //Increment and decrement table found
                    var largeTable = tables[largestTable];
                    tables.Remove(largeTable);
                    reserveTables.Add(largeTable);

                    //Decrement noSeats
                    tempSeats -= largeTable.NoSeats;
                }
            }

            for (int i = 0; tempSeats != 0; i++)
            {
                tempSeats -= tables[i].NoSeats;
                reserveTables.Add(tables[i]);
            }

            //Create a new order and get its orderId
            int orderId = CreateNewOrder(resId, noSeats, dateTime);

            //Reserve tables found in the reserveTables list --Handle concurrency

            return orderId;
        }

        private IEnumerable<Table> GetAvailableRestaurantTables(int resId)
        {
            TableDb tblDb = new TableDb();
            return ConvertTableListToModel(tblDb.GetAvailableRestaurantTables(resId));
        }

        private int CreateNewOrder(int resId, int noSeats, DateTime date)
        {
            OrderCtrl orderCtrl = new OrderCtrl();
            DateTime now = DateTime.Now;
            string nowDate = now.Year + "-" + now.Month + "-" + now.Day + " " + now.Hour +
                             ":" + now.Minute + ":" + now.Second + "." + now.Millisecond;

            string dateTime = date.Year + "-" + date.Month + "-" + date.Day + " " + date.Hour +
                              ":" + date.Minute + ":" + date.Second + "." + date.Millisecond;

            var order = new ModelLibrary.Order
            {
                RestaurantId = Convert.ToString(resId),
                DateTime = nowDate,
                ReservationDateTime = dateTime,
                NoSeats = Convert.ToString(noSeats),
                Accepted = false
            };
            return orderCtrl.AddOrder(order);
        }

        public Dictionary<int, int> GetAllModulo(List<Table> tables, int tempSeats)
        {
            Dictionary<int,int> modulo = new Dictionary<int, int>();
            int modItr = 0;

            foreach (var table in tables)
            {
                modulo[modItr] = tempSeats % table.NoSeats;
                modItr++;
            }

            return modulo;
        }

        public Dictionary<int,int> FindSmallestModulo(Dictionary<int,int> modulo)
        {
            int min = modulo.Min(entry => entry.Value);
            Dictionary<int,int> keyValuePair = new Dictionary<int, int>();
            foreach (var entry in modulo)
            {
                if(entry.Value == min)
                    keyValuePair.Add(entry.Key,entry.Value);
            }

            return keyValuePair;
        }

        public Dictionary<int, int> GetTablesSmallestModulo(Dictionary<int,int> smallDict, List<Table> tables)
        {
            var dictEnum = smallDict.GetEnumerator();
            var tablesSmallModulo = new Dictionary<int, int>();

            for (int j = 0; j < smallDict.Count; j++)
            {
                tablesSmallModulo.Add(dictEnum.Current.Key,tables[dictEnum.Current.Key].NoSeats);
                j++;
            }

            dictEnum.Dispose();

            return tablesSmallModulo;
        }

        //Returns the key used to find the table in the tables list, then remove it from the list
        public int GetLargestTable(Dictionary<int,int> tablesModulo)
        {
            int tableKey = 0;

            int maxNoSeats = tablesModulo.Max(entry => entry.Value);
            foreach (var table in tablesModulo)
            {
                if (table.Value == maxNoSeats)
                    tableKey = table.Key;
            }

            return tableKey;
        }
    }
}
