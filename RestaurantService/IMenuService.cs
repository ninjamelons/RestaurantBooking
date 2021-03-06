﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RestaurantService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMenuService" in both code and config file together.
    [ServiceContract]
    public interface IMenuService
    {

        [OperationContract]
        void CreateMenu(ModelLibrary.Menu menu);

        [OperationContract]
        void UpdateMenu(ModelLibrary.Menu beforeMenu, ModelLibrary.Menu afterMenu);

        [OperationContract]
        void DeleteMenu(int menuId);

        [OperationContract]
        IEnumerable<ModelLibrary.Menu> GetAllMenusByRestaurant(int restaurantId);

        [OperationContract]
        ModelLibrary.Menu GetMenuById(int menuId);

        [OperationContract]
        ModelLibrary.Menu GetMenuByName(string name);





    }
}
