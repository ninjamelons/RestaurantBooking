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

        public void CreatePrice(ModelLibrary.Price price, int itemId) //ModelLibrary.Price price, int itemId
        {
            var priceCtrl = new PriceCtrl();
            priceCtrl.CreatePrice(price, itemId);
            //priceCtrl.CreatePrice(price, itemId);
        }

        public void DeletePricesByItemId(int itemId)
        {
            var priceCtrl = new PriceCtrl();
            priceCtrl.DeletePricesByItemId(itemId);

        }

        public ModelLibrary.Price GetLatestPrice(int itemId)
        {
            var priceCtrl = new PriceCtrl();
            var priceDb = new PriceDb();
            return priceCtrl.ConvertPriceToModel(priceDb.GetLatestPriceById(itemId));
        }

        public void UpdatePrice(ModelLibrary.Price price, int itemId)
        {
            var priceCtrl = new PriceCtrl();
            priceCtrl.UpdatePrice(price, itemId);
        }
       
    }
}
