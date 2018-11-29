using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class PriceDb
    {
        public  void AddPrice(Price price)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            db.Prices.InsertOnSubmit(price);
            db.SubmitChanges();
        }
    

        public  Price GetPrice(int itemId, DateTime startDate)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var pric = db.Prices.Single(t => t.itemId == itemId  && t.startDate == startDate);
            return pric;
        }

        public  void DeletePrice(Price price, int itemId)
        {
            var db = new JustFeastDbDataContext();
            DatabaseAccessLibrary.Price dbPrice = db.Prices.First(t => t.itemId == itemId
                                                        && t.startDate == price.startDate);
            if (dbPrice != null)
            {
                db.Prices.DeleteOnSubmit(db.Prices.First(t => t.itemId == itemId
                                                        && t.startDate == price.startDate));
                db.SubmitChanges();
            }
        }

        public  void UpdatePrice(Price priceBefore, Price priceAfter)
        {
            var db = new JustFeastDbDataContext();
            var dbPrice = db.Prices.SingleOrDefault(t => t.itemId == priceBefore.itemId
                                                  && t.startDate == priceBefore.startDate);

            dbPrice.itemId = priceAfter.itemId;
            dbPrice.startDate = priceAfter.startDate;
            dbPrice.endDate = priceAfter.endDate;
            dbPrice.price1 = priceAfter.price1;
            db.SubmitChanges();
        }

        public Price GetPriceItemId(int itemId)
        {
            var db = new JustFeastDbDataContext();
            return db.Prices.FirstOrDefault(p => p.itemId == itemId);
        }

        public IEnumerable<Price> GetPrices()
        {
            var db = new JustFeastDbDataContext();

            var prices = db.Prices.AsEnumerable();
            return prices;
        }

    }
}
