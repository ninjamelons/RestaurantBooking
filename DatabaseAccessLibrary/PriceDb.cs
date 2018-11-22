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
        public static void AddPrice(Price price)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            if (db.Prices.Any(t => !(t.itemId == price.itemId && t.startDate == price.startDate || t.endDate == price.endDate))) //???????
                db.Prices.InsertOnSubmit(price);
            db.SubmitChanges();
        }
    

        public static Price GetPrice(int itemId, DateTime startDate)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var pric = db.Prices.Single(t => t.itemId == itemId  && t.startDate == startDate);
            return pric;
        }

        public static void DeletePrice(Price price)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            if (db.Prices.Any(t => t.itemId == price.itemId && t.startDate == price.startDate))
                db.Prices.DeleteOnSubmit(price);
            db.SubmitChanges();
        }

        public static void UpdatePrice(Price priceBefore, Price priceAfter)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var price = db.Prices.SingleOrDefault(t => t.itemId == priceBefore.itemId && t.startDate == priceBefore.startDate);
            price = priceAfter;
            db.SubmitChanges();
        }

        public IEnumerable<Price> GetPrices()
        {
            var db = new JustFeastDbDataContext();

            var prices = db.Prices.AsEnumerable();
            return prices;
        }

    }
}
