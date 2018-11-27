using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAccessLibrary;

namespace ControllerLibrary
{
    public class PriceCtrl
    {
        public DatabaseAccessLibrary.Price CreatePrice(ModelLibrary.Price price)
        {
            var priceDb = new PriceDb();
            var returnPrice = new DatabaseAccessLibrary.Price
            {
                endDate = price.EndDate,
                startDate = price.StartDate,
                price1 = price.VarPrice,
            };

            priceDb.AddPrice(returnPrice);

            return returnPrice;
        }

        public ModelLibrary.Price ConvertPriceToModel(DatabaseAccessLibrary.Price price)
        {
            var modelPrice = new ModelLibrary.Price
            {
                
                VarPrice = price.price1,
                StartDate = price.startDate,
                EndDate = price.endDate,
                
            };
            return modelPrice;
        }

        public  DatabaseAccessLibrary.Price ConvertPriceToDb(ModelLibrary.Price price)
        {
            var dbPrice = new DatabaseAccessLibrary.Price
            {
                price1 = price.VarPrice,
                startDate = price.StartDate,
                endDate = price.EndDate
            };
            return dbPrice;
        }

        public void UpdatePrice(ModelLibrary.Price beforePrice, ModelLibrary.Price afterPrice)
        {
            var priceDb = new PriceDb();

            var beforeDbPrice = ConvertPriceToDb(beforePrice);
            var afterDbPrice = ConvertPriceToDb(afterPrice);
            priceDb.UpdatePrice(beforeDbPrice, afterDbPrice);
        }

        public void DeletePrice(ModelLibrary.Price price)
        {
            var priceDb = new PriceDb();
            var dbPrice = ConvertPriceToDb(price);
            priceDb.DeletePrice(dbPrice);
        }
    }
}
