using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Transactions;
using DatabaseAccessLibrary;
using ModelLibrary;

namespace ControllerLibrary
{
    public class TableCtrl
    {

        private List<Table> tableListPerOrder = new List<Table>();
        private ResTable ConvertTable(Table table)
        {
            var returnTable = new ResTable();

            returnTable.noSeats = Convert.ToInt32(table.NoSeats);
            returnTable.reserved = table.Reserved;
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
                    TableId = tbl.id,
                    NoSeats = tbl.noSeats,
                    Reserved = tbl.reserved,
                    RestaurantId = tbl.restaurantId
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
                    TableId = table.id,
                    NoSeats = table.noSeats,
                    Reserved = table.reserved,
                    RestaurantId = table.restaurantId
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
                    TableId = table.id,
                    NoSeats = table.noSeats,
                    Reserved = table.reserved,
                    RestaurantId = table.restaurantId
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

        public void DeleteTable(Table table)
        {
            var tableDb = new TableDb();
            var resTable = ConvertTable(table);
            tableDb.DeleteTable(resTable);
        }

        private Table ConvertTableToModel(ResTable table)
        {
            var modelTable = new Table();

            modelTable.TableId = table.id;
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
            var tableDb = new TableDb();
            int orderId = 0;

            try
            {
                //CONCURRENCY -- makes a transaction that stops others from entering???
                using (var scope = new TransactionScope())
                {
                    var tables = (List<Table>) GetAvailableRestaurantTables(resId, dateTime);

                    //Algorithm to find suitable tables
                    //Reserve tables found in the reserveTables list --Handle concurrency
                    var reserveTablesIds = ConvertTablesToDb(LeastNumberOfTables(tables, noSeats));

                    //Reserve the tables in the database -- Concurrency in the database layer
                    try
                    {
                        //Create a new order and get its orderId
                        orderId = CreateNewOrder(resId, noSeats, dateTime);
                        tableDb.ReserveTables(reserveTablesIds, orderId);
                    }
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    catch(TransactionManagerCommunicationException e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    scope.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                Console.WriteLine("TransactionAbortedException Message: {0}", ex.Message);
                return 0;
            }

            return orderId;
        }

        private IEnumerable<ResTable> ConvertTablesToDb(List<Table> tables)
        {
            List<ResTable> resTables = new List<ResTable>();
            foreach (var table in tables)
            {
                resTables.Add(new ResTable
                {
                    id = table.TableId, noSeats = table.NoSeats
                });
            }
            return resTables;
        }

        public void ReserveSingleTable(int tableId, int resId)
        {
            var tblDb = new TableDb();
            try
            {
                using (var scope = new TransactionScope())
                {
                    var orderId = CreateNewOrder(resId, 0, DateTime.Now);
                    tblDb.ReserveSingleTable(tableId, orderId);
                    scope.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                Console.WriteLine("TransactionAbortedException Message: {0}", ex.Message);
                throw;
            }
        }

        public IEnumerable<Table> GetTablesWithReserved(int resId)
        {
            var tblDb = new TableDb();
            var tables = ConvertTableListToModel(tblDb.GetTablesWithReserved(resId));
            return tables;
        }

        private IEnumerable<Table> GetAvailableRestaurantTables(int resId, DateTime dateTime)
        {
            TableDb tblDb = new TableDb();
            return ConvertTableListToModel(tblDb.GetAvailableRestaurantTables(resId, dateTime));
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
                DateTime = Convert.ToString(now),
                ReservationDateTime = dateTime,
                //DateTime = nowDate,
                //ReservationDateTime = dateTime,
                NoSeats = Convert.ToString(noSeats),
                Accepted = false
            };
            return orderCtrl.AddOrder(order);
        }

        public List<Table> LeastNumberOfTables(List<Table> tables, int tempSeats)
        {
            int modItr = 0;
            var seatsLeftInOrder = new int[tables.Count];

            foreach (var table in tables)
            {
                seatsLeftInOrder[modItr] = tempSeats - table.NoSeats;
                modItr++;
            }

            int index = -1;
            int mostSeats = 0;
            for (int i = 0; i < seatsLeftInOrder.Length; i++)
            {
                if (seatsLeftInOrder[i] <= mostSeats && -2 < seatsLeftInOrder[i])
                {
                    mostSeats = seatsLeftInOrder[i];
                    if (mostSeats == 0)
                    {
                        index = i;
                        break;
                    }
                    index = i;
                }
                else if(i == seatsLeftInOrder.Length - 1)
                {
                    index = i;
                }
            }

            tableListPerOrder.Add(tables[index]);
            tables.RemoveAt(index);
            if (seatsLeftInOrder[index] > 0)
            {
                LeastNumberOfTables(tables, seatsLeftInOrder[index]);
            }

            return tableListPerOrder;
        }
    }
}
