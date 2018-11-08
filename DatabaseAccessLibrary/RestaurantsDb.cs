using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class RestaurantsDb
    {
        public void AddTable(ResTable resTable)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            if(db.ResTables.Any(t => !(t.noSeats == resTable.noSeats && t.restaurantId == resTable.restaurantId)))
                db.ResTables.InsertOnSubmit(resTable);

        }

        public ResTable GetTable(int noSeats, int reserved, int restaurantId, int total)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var resTable = db.ResTables.Single(t => t.noSeats == noSeats
                                                    && t.reserved == reserved
                                                    && t.restaurantId == restaurantId
                                                    && t.total == total);
            return resTable;
        }
    }
}
