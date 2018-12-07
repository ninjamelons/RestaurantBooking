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
    
        public Price GetLatestPriceById(int itemId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var prico = db.Prices.Where(t => t.itemId == itemId).OrderByDescending(t => t.startDate);
            var pric = prico.First();
            return pric;
        }
        public  Price GetPriceByIdAndStartDate(int itemId, DateTime startDate)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();

            var pric = db.Prices.Single(t => t.itemId == itemId  && t.startDate == startDate);
            return pric;
        }

        public void DeletePricesByItemId(int itemId)
        {
            var db = new JustFeastDbDataContext();
            var dbPrice = db.Prices.Where(t => t.itemId == itemId);
            if (dbPrice != null)
            {
                db.Prices.DeleteAllOnSubmit(dbPrice);
                db.SubmitChanges();
            }
        }

        public  void UpdatePrice(Price price)
        {
            var db = new JustFeastDbDataContext();
            var pric = db.Prices.FirstOrDefault(x => x.itemId == price.itemId );

            pric.price1 = price.price1;

            db.SubmitChanges();
        }

        public Price GetPriceItemId(int itemId)
        {
            var db = new JustFeastDbDataContext();
            return db.Prices.FirstOrDefault(p => p.itemId == itemId);
        }

        public IEnumerable<Price> GetPriceItemIdList(int itemId)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            return db.Prices.Where(t => t.itemId == itemId).OrderByDescending(t=> t.startDate);
        }

    }
}
