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
        public ModelLibrary.Price CreatePrice(ModelLibrary.Price price, int itemId)
        {
            var priceDb = new PriceDb();
            var returnPrice = new ModelLibrary.Price
            {
                EndDate = price.EndDate,
                StartDate = price.StartDate,
                VarPrice = price.VarPrice
            };

            var dbPrice = new DatabaseAccessLibrary.Price
            {
                endDate =  price.EndDate,
                startDate = price.StartDate,
                price1 = price.VarPrice,
                itemId  = itemId
            };
            priceDb.AddPrice(dbPrice);

            return returnPrice;
        }

        public ModelLibrary.Price ConvertPriceToModel(DatabaseAccessLibrary.Price price)
        {
            if (price == null)
                return null;

            var modelPrice = new ModelLibrary.Price
            {
                
                VarPrice = price.price1,
                StartDate = price.startDate,
                EndDate = price.endDate,

            };
            return modelPrice;
        }

        public  DatabaseAccessLibrary.Price ConvertPriceToDb(ModelLibrary.Price price, int itemId)
        {
            var priceDb = new PriceDb();
            var prices = priceDb.GetPriceItemIdList(itemId);

            var pric = prices.First();
            if (pric == null) return null;
            var dbPrice = new DatabaseAccessLibrary.Price
            {
                price1 = price.VarPrice,
                startDate = price.StartDate,
                endDate = price.EndDate,
                itemId = pric.itemId

            };
            return dbPrice;
        }

        public void UpdatePrice(ModelLibrary.Price newPrice, int itemId)
        {
            var priceDb = new PriceDb();

            var updPrice = ConvertPriceToDb(newPrice, itemId);
            priceDb.UpdatePrice(updPrice);
        }

        internal ModelLibrary.Price GetPriceItemId(int itemId)
        {
            var priceDb = new PriceDb();
            return ConvertPriceToModel(priceDb.GetPriceItemId(itemId));
        }
        public ModelLibrary.Price GetLatestPriceById(int itemId)
        {
            PriceDb priceDb = new PriceDb();
            return ConvertPriceToModel(priceDb.GetLatestPriceById(itemId));
        }

        public void DeletePricesByItemId(int itemId)
        {
            var priceDb = new PriceDb();
            priceDb.DeletePricesByItemId(itemId);
        }
    }
}
