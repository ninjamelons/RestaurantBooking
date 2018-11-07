using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DatabaseAccessLibrary;

namespace RestaurantBookingService.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRestaurantService" in both code and config file together.
    [ServiceContract]
    public interface IRestaurantService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        IEnumerable<Restaurant> GetAllRestaurants();
        [OperationContract]
        void RegisterRestaurant(Restaurant restaurant);

        [OperationContract]
        void CreateTable(ResTable resTable);
        [OperationContract]
        IEnumerable<ResTable> GetAllTables(Restaurant restaurant);
        [OperationContract]
        void UpdateTable(ResTable resTable);
        [OperationContract]
        void DeleteTable(ResTable resTable);
    }


}
