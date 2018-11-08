using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAccessLibrary;
using ModelLibrary;

namespace ControllerLibrary
{
    public class RestaurantCtrl
    {
        public ResTable ConvertTable(Table table)
        {
            ResTable returnTable = null;

            returnTable.noSeats = Convert.ToInt32(table.NoSeats);
            returnTable.reserved = Convert.ToInt32(table.Reserved);
            returnTable.total = Convert.ToInt32(table.Total);
            returnTable.restaurantId = Convert.ToInt32(table.RestaurantId);

            return returnTable;
        }
    }
}
