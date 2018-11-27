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
        PriceCtrl priceCtrl = new PriceCtrl();
        PriceDb priceDb = new PriceDb();
        public void CreatePrice(ModelLibrary.Price price)
        {
            priceCtrl.CreatePrice(price);
        }

        public void DeletePrice(ModelLibrary.Price price)
        {
            var dbPrice = priceCtrl.ConvertPriceToDb(price);
            priceDb.DeletePrice(dbPrice);
        }

        public IEnumerable<ModelLibrary.Price> GetAllPricesByItem(string id)
        {
            JustFeastDbDataContext db = new JustFeastDbDataContext();
            var price = db.Prices.ToList();
            List<ModelLibrary.Price> modelPrices = new List<ModelLibrary.Price>();
            foreach (var a in price)
            {
                modelPrices.Add(priceCtrl.ConvertPriceToModel(a));
            }
            return modelPrices;
        }

        public void UpdatePrice(ModelLibrary.Price beforePrice, ModelLibrary.Price afterPrice)
        {

            priceCtrl.UpdatePrice(beforePrice, afterPrice);
        }
       
    }
}
