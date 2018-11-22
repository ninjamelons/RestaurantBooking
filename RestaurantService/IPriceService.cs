using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RestaurantService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPriceService" in both code and config file together.
    [ServiceContract]
    public interface IPriceService
    {
        [OperationContract]
        void CreatePrice(ModelLibrary.Price price);

        [OperationContract]
        void UpdatePrice(ModelLibrary.Price price);

        [OperationContract]
        void DeletePrice(ModelLibrary.Price price);

        [OperationContract]
        IEnumerable<ModelLibrary.Price> GetAllPricesByItem(string id);

    }
}
