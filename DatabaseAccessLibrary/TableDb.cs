using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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

        //Returns the available restaurant tables within 1 hour of reservation
        public IEnumerable<ResTable> GetAvailableRestaurantTables(int resId, DateTime dateTime)
        {
            //Check if the tables are already reserved for the time
            //If the dateTime is between the reservation time and the reservation time + 1 hour
            //  then it should fail
            var order = db.Orders.Where(o => o.reservation <= dateTime 
                                             && dateTime <= o.reservation.Value.AddHours(1));

            //List of ResTables
            var unavailableTables = new List<ResTable>();
            if (order.Any())
            {
                foreach (var o in order)
                {
                    //List of ReservedTables where their orderId matches an id found in a conflicting time frame
                    var reservedTables = db.ReservedTables.Where(rt => rt.orderId == o.id);
                    //Populates list of ResTables using ReservedTables ids
                    foreach (var table in reservedTables)
                    {
                        unavailableTables.Add(db.ResTables.FirstOrDefault(t => t.id == table.tableId));
                    }
                }
            }
            //Get all tables for said restaurant
            var tables = GetRestaurantTables(resId).ToList();

            //Get all tables for restaurant that aren't booked in a one hour time slot
            var availableTables = tables.Except(unavailableTables).ToList();

            return availableTables;
        }

        public void ReserveTables(IEnumerable<ResTable> reserveTables, int orderId)
        {
            foreach (var table in reserveTables)
            {
                var reservedTable = new ReservedTable();
                reservedTable.orderId = orderId;
                reservedTable.tableId = table.id;
                db.ReservedTables.InsertOnSubmit(reservedTable);
            }
            db.SubmitChanges();
        }

        public void ReserveSingleTable(int tableId, int orderId)
        {
            var db = new JustFeastDbDataContext();
            db.ReservedTables.InsertOnSubmit(new ReservedTable
            {
                orderId = orderId, tableId = tableId
            });
            db.SubmitChanges();
        }

        public IEnumerable<ResTable> GetTablesWithReserved(int resId)
        {
            var orders = db.Orders.Where(o => o.reservation <= DateTime.Now 
                                             && DateTime.Now <= o.reservation.Value.AddHours(1));
            
            var unavailableTables = new List<ResTable>();
            foreach (var order in orders)
            {
                var reservedTables = db.ReservedTables.Where(rt => rt.orderId == order.id);
                foreach (var rt in reservedTables)
                {
                    var table = db.ResTables.FirstOrDefault(t => t.id == rt.tableId);
                    table.reserved = true;
                    unavailableTables.Add(table);
                }
            }
            
            //Get all tables for said restaurant
            var allTables = GetRestaurantTables(resId).ToList();

            //Get all tables for restaurant that aren't booked in a one hour time slot
            var availableTables = allTables.Except(unavailableTables).ToList();
            
            foreach (var table in availableTables)
            {
                table.reserved = false;
            }

            //var tables = availableTables.Union(unavailableTables);
            
            return availableTables;
        }
        
        public void AddTable(ResTable resTable)
        {
            var db = new JustFeastDbDataContext();

            if (db.ResTables.FirstOrDefault(t => (t.restaurantId == resTable.restaurantId)) != null)
            {
                db.ResTables.InsertOnSubmit(resTable);
                db.SubmitChanges();
            }
        }

        public ResTable GetTable(int noSeats, int restaurantId)
        {
            var db = new JustFeastDbDataContext();

            ResTable resTable = null;
            resTable = db.ResTables.FirstOrDefault(t => t.noSeats == noSeats
                                                && t.restaurantId == restaurantId);
            return resTable;
        }

        public IEnumerable<ResTable> GetTables()
        {
            var db = new JustFeastDbDataContext();

            var resTables = db.ResTables.AsEnumerable();
            return resTables;
        }


        public IEnumerable<ResTable> GetTablesWithReservedInTheFuture(int resId)
        {
            var orders = db.Orders.Where(o => o.reservation >= DateTime.Now);

            var unavailableTables = new List<ResTable>();
            foreach (var order in orders)
            {
                var reservedTables = db.ReservedTables.Where(rt => rt.orderId == order.id);
                foreach (var rt in reservedTables)
                {
                    var table = db.ResTables.FirstOrDefault(t => t.id == rt.tableId);
                    table.reserved = true;
                    unavailableTables.Add(table);
                }
            }
            return unavailableTables;
        }

        public bool DeleteTable(ResTable resTable)
        {
            var db = new JustFeastDbDataContext();
            ResTable tableRes = db.ResTables.First(t => t.noSeats == resTable.noSeats 
                                                        && t.restaurantId == resTable.restaurantId);
            var deleted = false;
            if (tableRes != null)
            {
                var enumTables = GetTablesWithReservedInTheFuture(resTable.restaurantId);
                foreach (var table in enumTables)
                {
                    if (table.reserved && resTable.noSeats == table.noSeats)
                    {
                        using (var transaction = new TransactionScope())
                        {
                            try
                            {

                                db.ResTables.DeleteOnSubmit(db.ResTables.First(t => t.id == table.id));
                                db.ReservedTables.DeleteOnSubmit(db.ReservedTables.First(t => t.tableId == table.id));
                                db.SubmitChanges();
                                transaction.Complete();
                                deleted = true;
                                break;
                            }
                            catch (Exception ex)
                            {
                                deleted = false;
                            }
                        }
                    }
                }
            }

            return deleted;
        }
    }
}
