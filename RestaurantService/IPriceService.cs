﻿using System;
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
        void CreatePrice(ModelLibrary.Price price, int itemId);

        [OperationContract]
        void DeletePricesByItemId(int itemId);
        [OperationContract]
        ModelLibrary.Price GetLatestPrice(int itemId);

        [OperationContract]
        void UpdatePrice(ModelLibrary.Price price, int itemId);

    }
}
