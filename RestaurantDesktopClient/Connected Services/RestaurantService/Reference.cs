﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestaurantDesktopClient.RestaurantService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RestaurantService.IRestaurantService")]
    public interface IRestaurantService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetAllRestaurants", ReplyAction="http://tempuri.org/IRestaurantService/GetAllRestaurantsResponse")]
        ModelLibrary.Restaurant[] GetAllRestaurants();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetAllRestaurants", ReplyAction="http://tempuri.org/IRestaurantService/GetAllRestaurantsResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Restaurant[]> GetAllRestaurantsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/RegisterRestaurant", ReplyAction="http://tempuri.org/IRestaurantService/RegisterRestaurantResponse")]
        void RegisterRestaurant(ModelLibrary.Restaurant restaurant);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/RegisterRestaurant", ReplyAction="http://tempuri.org/IRestaurantService/RegisterRestaurantResponse")]
        System.Threading.Tasks.Task RegisterRestaurantAsync(ModelLibrary.Restaurant restaurant);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/CreateTable", ReplyAction="http://tempuri.org/IRestaurantService/CreateTableResponse")]
        void CreateTable(ModelLibrary.Table table);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/CreateTable", ReplyAction="http://tempuri.org/IRestaurantService/CreateTableResponse")]
        System.Threading.Tasks.Task CreateTableAsync(ModelLibrary.Table table);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetAllTables", ReplyAction="http://tempuri.org/IRestaurantService/GetAllTablesResponse")]
        ModelLibrary.Table[] GetAllTables(ModelLibrary.Restaurant restaurant);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetAllTables", ReplyAction="http://tempuri.org/IRestaurantService/GetAllTablesResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Table[]> GetAllTablesAsync(ModelLibrary.Restaurant restaurant);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetTable", ReplyAction="http://tempuri.org/IRestaurantService/GetTableResponse")]
        ModelLibrary.Table[] GetTable(ModelLibrary.Table table);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetTable", ReplyAction="http://tempuri.org/IRestaurantService/GetTableResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Table[]> GetTableAsync(ModelLibrary.Table table);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/UpdateTable", ReplyAction="http://tempuri.org/IRestaurantService/UpdateTableResponse")]
        void UpdateTable(ModelLibrary.Table table);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/UpdateTable", ReplyAction="http://tempuri.org/IRestaurantService/UpdateTableResponse")]
        System.Threading.Tasks.Task UpdateTableAsync(ModelLibrary.Table table);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/DeleteTable", ReplyAction="http://tempuri.org/IRestaurantService/DeleteTableResponse")]
        void DeleteTable(ModelLibrary.Table table);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/DeleteTable", ReplyAction="http://tempuri.org/IRestaurantService/DeleteTableResponse")]
        System.Threading.Tasks.Task DeleteTableAsync(ModelLibrary.Table table);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRestaurantServiceChannel : RestaurantDesktopClient.RestaurantService.IRestaurantService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RestaurantServiceClient : System.ServiceModel.ClientBase<RestaurantDesktopClient.RestaurantService.IRestaurantService>, RestaurantDesktopClient.RestaurantService.IRestaurantService {
        
        public RestaurantServiceClient() {
        }
        
        public RestaurantServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RestaurantServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RestaurantServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RestaurantServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ModelLibrary.Restaurant[] GetAllRestaurants() {
            return base.Channel.GetAllRestaurants();
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Restaurant[]> GetAllRestaurantsAsync() {
            return base.Channel.GetAllRestaurantsAsync();
        }
        
        public void RegisterRestaurant(ModelLibrary.Restaurant restaurant) {
            base.Channel.RegisterRestaurant(restaurant);
        }
        
        public System.Threading.Tasks.Task RegisterRestaurantAsync(ModelLibrary.Restaurant restaurant) {
            return base.Channel.RegisterRestaurantAsync(restaurant);
        }
        
        public void CreateTable(ModelLibrary.Table table) {
            base.Channel.CreateTable(table);
        }
        
        public System.Threading.Tasks.Task CreateTableAsync(ModelLibrary.Table table) {
            return base.Channel.CreateTableAsync(table);
        }
        
        public ModelLibrary.Table[] GetAllTables(ModelLibrary.Restaurant restaurant) {
            return base.Channel.GetAllTables(restaurant);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Table[]> GetAllTablesAsync(ModelLibrary.Restaurant restaurant) {
            return base.Channel.GetAllTablesAsync(restaurant);
        }
        
        public ModelLibrary.Table[] GetTable(ModelLibrary.Table table) {
            return base.Channel.GetTable(table);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Table[]> GetTableAsync(ModelLibrary.Table table) {
            return base.Channel.GetTableAsync(table);
        }
        
        public void UpdateTable(ModelLibrary.Table table) {
            base.Channel.UpdateTable(table);
        }
        
        public System.Threading.Tasks.Task UpdateTableAsync(ModelLibrary.Table table) {
            return base.Channel.UpdateTableAsync(table);
        }
        
        public void DeleteTable(ModelLibrary.Table table) {
            base.Channel.DeleteTable(table);
        }
        
        public System.Threading.Tasks.Task DeleteTableAsync(ModelLibrary.Table table) {
            return base.Channel.DeleteTableAsync(table);
        }
    }
}
