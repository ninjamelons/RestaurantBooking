using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAccessLibrary;
using Price = ModelLibrary.Price;

namespace ControllerLibrary
{
    public class PriceCtrl
    {
        public static ModelLibrary.Price CreatePrice(double price, DateTime startDate, DateTime endDate)
        {
            var modelPrice = new ModelLibrary.Price
            {
                VarPrice = price,
                StartDate = startDate,
                EndDate = endDate
            };
            return modelPrice;
        }

        public static ModelLibrary.Price ConvertPriceToModel(DatabaseAccessLibrary.Price price)
        {
            var modelPrice = new ModelLibrary.Price
            {
                VarPrice = price.price1,
                StartDate = price.startDate,
                EndDate = price.endDate,
                
            };
            return modelPrice;
        }

        public static DatabaseAccessLibrary.Price ConvertPriceToDb(ModelLibrary.Price price)
        {
            var dbPrice = new DatabaseAccessLibrary.Price
            {
                price1 = price.VarPrice,
                startDate = price.StartDate,
                endDate = price.EndDate
            };
            return dbPrice;
        }

        internal Price GetPriceItemId(int id)
        {
            var priceDb = new PriceDb();
            return ConvertPriceToModel(priceDb.GetPriceItemId(id));
        }
    }
}
