using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ControllerLibrary;
using DatabaseAccessLibrary;
using ModelLibrary;

namespace RestaurantService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PriceService" in both code and config file together.
    public class PriceService : IPriceService
    {
        public void CreatePrice(ModelLibrary.Price price)
        {

            var dbPrice = PriceCtrl.ConvertPriceToDb(price);
            PriceDb.AddPrice(dbPrice);
        }

        public void DeletePrice(ModelLibrary.Price price)
        {
            var dbPrice = PriceCtrl.ConvertPriceToDb(price);
            PriceDb.DeletePrice(dbPrice);
        }

        public IEnumerable<ModelLibrary.Price> GetAllPricesByItem(string id)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var price = db.Prices.ToList();
            List<ModelLibrary.Price> modelPrices = new List<ModelLibrary.Price>();
            foreach (var a in price)
            {
                modelPrices.Add(PriceCtrl.ConvertPriceToModel(a));
            }
            return modelPrices;
        }

        public void UpdatePrice(ModelLibrary.Price price)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var aPrice = db.Prices.SingleOrDefault(a => a.itemId == price.ItemId);
            aPrice = PriceCtrl.ConvertPriceToDb(price);
            db.SubmitChanges();
        }
    }
}
