﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UserWebClient.RestaurantService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RestaurantService.IRestaurantService")]
    public interface IRestaurantService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetAllRestaurants", ReplyAction="http://tempuri.org/IRestaurantService/GetAllRestaurantsResponse")]
        ModelLibrary.Restaurant[] GetAllRestaurants();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetAllRestaurants", ReplyAction="http://tempuri.org/IRestaurantService/GetAllRestaurantsResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Restaurant[]> GetAllRestaurantsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetAllRestaurantsByCategory", ReplyAction="http://tempuri.org/IRestaurantService/GetAllRestaurantsByCategoryResponse")]
        ModelLibrary.Restaurant[] GetAllRestaurantsByCategory(int categoryId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetAllRestaurantsByCategory", ReplyAction="http://tempuri.org/IRestaurantService/GetAllRestaurantsByCategoryResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Restaurant[]> GetAllRestaurantsByCategoryAsync(int categoryId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetAllRestaurantsByZipCode", ReplyAction="http://tempuri.org/IRestaurantService/GetAllRestaurantsByZipCodeResponse")]
        ModelLibrary.Restaurant[] GetAllRestaurantsByZipCode(int zipcode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetAllRestaurantsByZipCode", ReplyAction="http://tempuri.org/IRestaurantService/GetAllRestaurantsByZipCodeResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Restaurant[]> GetAllRestaurantsByZipCodeAsync(int zipcode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetRestaurantsPaged", ReplyAction="http://tempuri.org/IRestaurantService/GetRestaurantsPagedResponse")]
        ModelLibrary.Restaurant[] GetRestaurantsPaged(int zipcode, int categoryId, int page, int amount, bool verified, bool discontinued);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetRestaurantsPaged", ReplyAction="http://tempuri.org/IRestaurantService/GetRestaurantsPagedResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Restaurant[]> GetRestaurantsPagedAsync(int zipcode, int categoryId, int page, int amount, bool verified, bool discontinued);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/CreateRestaurant", ReplyAction="http://tempuri.org/IRestaurantService/CreateRestaurantResponse")]
        void CreateRestaurant(ModelLibrary.Restaurant restaurant);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/CreateRestaurant", ReplyAction="http://tempuri.org/IRestaurantService/CreateRestaurantResponse")]
        System.Threading.Tasks.Task CreateRestaurantAsync(ModelLibrary.Restaurant restaurant);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/UpdateRestaurant", ReplyAction="http://tempuri.org/IRestaurantService/UpdateRestaurantResponse")]
        void UpdateRestaurant(ModelLibrary.Restaurant restaurant);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/UpdateRestaurant", ReplyAction="http://tempuri.org/IRestaurantService/UpdateRestaurantResponse")]
        System.Threading.Tasks.Task UpdateRestaurantAsync(ModelLibrary.Restaurant restaurant);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/DeleteRestaurant", ReplyAction="http://tempuri.org/IRestaurantService/DeleteRestaurantResponse")]
        void DeleteRestaurant(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/DeleteRestaurant", ReplyAction="http://tempuri.org/IRestaurantService/DeleteRestaurantResponse")]
        System.Threading.Tasks.Task DeleteRestaurantAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetAllRestaurantCategories", ReplyAction="http://tempuri.org/IRestaurantService/GetAllRestaurantCategoriesResponse")]
        ModelLibrary.RestaurantCategory[] GetAllRestaurantCategories();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetAllRestaurantCategories", ReplyAction="http://tempuri.org/IRestaurantService/GetAllRestaurantCategoriesResponse")]
        System.Threading.Tasks.Task<ModelLibrary.RestaurantCategory[]> GetAllRestaurantCategoriesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetRestaurant", ReplyAction="http://tempuri.org/IRestaurantService/GetRestaurantResponse")]
        ModelLibrary.Restaurant GetRestaurant(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetRestaurant", ReplyAction="http://tempuri.org/IRestaurantService/GetRestaurantResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Restaurant> GetRestaurantAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/CreateRestaurantCategory", ReplyAction="http://tempuri.org/IRestaurantService/CreateRestaurantCategoryResponse")]
        void CreateRestaurantCategory(ModelLibrary.RestaurantCategory res);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/CreateRestaurantCategory", ReplyAction="http://tempuri.org/IRestaurantService/CreateRestaurantCategoryResponse")]
        System.Threading.Tasks.Task CreateRestaurantCategoryAsync(ModelLibrary.RestaurantCategory res);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/DeleteRestaurantCategory", ReplyAction="http://tempuri.org/IRestaurantService/DeleteRestaurantCategoryResponse")]
        void DeleteRestaurantCategory(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/DeleteRestaurantCategory", ReplyAction="http://tempuri.org/IRestaurantService/DeleteRestaurantCategoryResponse")]
        System.Threading.Tasks.Task DeleteRestaurantCategoryAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/UpdateRestaurantCategory", ReplyAction="http://tempuri.org/IRestaurantService/UpdateRestaurantCategoryResponse")]
        void UpdateRestaurantCategory(ModelLibrary.RestaurantCategory res);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/UpdateRestaurantCategory", ReplyAction="http://tempuri.org/IRestaurantService/UpdateRestaurantCategoryResponse")]
        System.Threading.Tasks.Task UpdateRestaurantCategoryAsync(ModelLibrary.RestaurantCategory res);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetRestaurantCategory", ReplyAction="http://tempuri.org/IRestaurantService/GetRestaurantCategoryResponse")]
        ModelLibrary.RestaurantCategory GetRestaurantCategory(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetRestaurantCategory", ReplyAction="http://tempuri.org/IRestaurantService/GetRestaurantCategoryResponse")]
        System.Threading.Tasks.Task<ModelLibrary.RestaurantCategory> GetRestaurantCategoryAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/CreateTable", ReplyAction="http://tempuri.org/IRestaurantService/CreateTableResponse")]
        void CreateTable(ModelLibrary.Table table);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/CreateTable", ReplyAction="http://tempuri.org/IRestaurantService/CreateTableResponse")]
        System.Threading.Tasks.Task CreateTableAsync(ModelLibrary.Table table);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetAllTables", ReplyAction="http://tempuri.org/IRestaurantService/GetAllTablesResponse")]
        ModelLibrary.Table[] GetAllTables(int restaurantId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetAllTables", ReplyAction="http://tempuri.org/IRestaurantService/GetAllTablesResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Table[]> GetAllTablesAsync(int restaurantId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetTable", ReplyAction="http://tempuri.org/IRestaurantService/GetTableResponse")]
        ModelLibrary.Table GetTable(ModelLibrary.Table table);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/GetTable", ReplyAction="http://tempuri.org/IRestaurantService/GetTableResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Table> GetTableAsync(ModelLibrary.Table table);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/UpdateTable", ReplyAction="http://tempuri.org/IRestaurantService/UpdateTableResponse")]
        void UpdateTable(ModelLibrary.Table oldTable, ModelLibrary.Table newTable);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/UpdateTable", ReplyAction="http://tempuri.org/IRestaurantService/UpdateTableResponse")]
        System.Threading.Tasks.Task UpdateTableAsync(ModelLibrary.Table oldTable, ModelLibrary.Table newTable);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/DeleteTable", ReplyAction="http://tempuri.org/IRestaurantService/DeleteTableResponse")]
        void DeleteTable(ModelLibrary.Table table);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestaurantService/DeleteTable", ReplyAction="http://tempuri.org/IRestaurantService/DeleteTableResponse")]
        System.Threading.Tasks.Task DeleteTableAsync(ModelLibrary.Table table);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRestaurantServiceChannel : UserWebClient.RestaurantService.IRestaurantService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RestaurantServiceClient : System.ServiceModel.ClientBase<UserWebClient.RestaurantService.IRestaurantService>, UserWebClient.RestaurantService.IRestaurantService {
        
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
        
        public ModelLibrary.Restaurant[] GetAllRestaurantsByCategory(int categoryId) {
            return base.Channel.GetAllRestaurantsByCategory(categoryId);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Restaurant[]> GetAllRestaurantsByCategoryAsync(int categoryId) {
            return base.Channel.GetAllRestaurantsByCategoryAsync(categoryId);
        }
        
        public ModelLibrary.Restaurant[] GetAllRestaurantsByZipCode(int zipcode) {
            return base.Channel.GetAllRestaurantsByZipCode(zipcode);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Restaurant[]> GetAllRestaurantsByZipCodeAsync(int zipcode) {
            return base.Channel.GetAllRestaurantsByZipCodeAsync(zipcode);
        }
        
        public ModelLibrary.Restaurant[] GetRestaurantsPaged(int zipcode, int categoryId, int page, int amount, bool verified, bool discontinued) {
            return base.Channel.GetRestaurantsPaged(zipcode, categoryId, page, amount, verified, discontinued);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Restaurant[]> GetRestaurantsPagedAsync(int zipcode, int categoryId, int page, int amount, bool verified, bool discontinued) {
            return base.Channel.GetRestaurantsPagedAsync(zipcode, categoryId, page, amount, verified, discontinued);
        }
        
        public void CreateRestaurant(ModelLibrary.Restaurant restaurant) {
            base.Channel.CreateRestaurant(restaurant);
        }
        
        public System.Threading.Tasks.Task CreateRestaurantAsync(ModelLibrary.Restaurant restaurant) {
            return base.Channel.CreateRestaurantAsync(restaurant);
        }
        
        public void UpdateRestaurant(ModelLibrary.Restaurant restaurant) {
            base.Channel.UpdateRestaurant(restaurant);
        }
        
        public System.Threading.Tasks.Task UpdateRestaurantAsync(ModelLibrary.Restaurant restaurant) {
            return base.Channel.UpdateRestaurantAsync(restaurant);
        }
        
        public void DeleteRestaurant(int id) {
            base.Channel.DeleteRestaurant(id);
        }
        
        public System.Threading.Tasks.Task DeleteRestaurantAsync(int id) {
            return base.Channel.DeleteRestaurantAsync(id);
        }
        
        public ModelLibrary.RestaurantCategory[] GetAllRestaurantCategories() {
            return base.Channel.GetAllRestaurantCategories();
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.RestaurantCategory[]> GetAllRestaurantCategoriesAsync() {
            return base.Channel.GetAllRestaurantCategoriesAsync();
        }
        
        public ModelLibrary.Restaurant GetRestaurant(int id) {
            return base.Channel.GetRestaurant(id);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Restaurant> GetRestaurantAsync(int id) {
            return base.Channel.GetRestaurantAsync(id);
        }
        
        public void CreateRestaurantCategory(ModelLibrary.RestaurantCategory res) {
            base.Channel.CreateRestaurantCategory(res);
        }
        
        public System.Threading.Tasks.Task CreateRestaurantCategoryAsync(ModelLibrary.RestaurantCategory res) {
            return base.Channel.CreateRestaurantCategoryAsync(res);
        }
        
        public void DeleteRestaurantCategory(int id) {
            base.Channel.DeleteRestaurantCategory(id);
        }
        
        public System.Threading.Tasks.Task DeleteRestaurantCategoryAsync(int id) {
            return base.Channel.DeleteRestaurantCategoryAsync(id);
        }
        
        public void UpdateRestaurantCategory(ModelLibrary.RestaurantCategory res) {
            base.Channel.UpdateRestaurantCategory(res);
        }
        
        public System.Threading.Tasks.Task UpdateRestaurantCategoryAsync(ModelLibrary.RestaurantCategory res) {
            return base.Channel.UpdateRestaurantCategoryAsync(res);
        }
        
        public ModelLibrary.RestaurantCategory GetRestaurantCategory(int id) {
            return base.Channel.GetRestaurantCategory(id);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.RestaurantCategory> GetRestaurantCategoryAsync(int id) {
            return base.Channel.GetRestaurantCategoryAsync(id);
        }
        
        public void CreateTable(ModelLibrary.Table table) {
            base.Channel.CreateTable(table);
        }
        
        public System.Threading.Tasks.Task CreateTableAsync(ModelLibrary.Table table) {
            return base.Channel.CreateTableAsync(table);
        }
        
        public ModelLibrary.Table[] GetAllTables(int restaurantId) {
            return base.Channel.GetAllTables(restaurantId);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Table[]> GetAllTablesAsync(int restaurantId) {
            return base.Channel.GetAllTablesAsync(restaurantId);
        }
        
        public ModelLibrary.Table GetTable(ModelLibrary.Table table) {
            return base.Channel.GetTable(table);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Table> GetTableAsync(ModelLibrary.Table table) {
            return base.Channel.GetTableAsync(table);
        }
        
        public void UpdateTable(ModelLibrary.Table oldTable, ModelLibrary.Table newTable) {
            base.Channel.UpdateTable(oldTable, newTable);
        }
        
        public System.Threading.Tasks.Task UpdateTableAsync(ModelLibrary.Table oldTable, ModelLibrary.Table newTable) {
            return base.Channel.UpdateTableAsync(oldTable, newTable);
        }
        
        public void DeleteTable(ModelLibrary.Table table) {
            base.Channel.DeleteTable(table);
        }
        
        public System.Threading.Tasks.Task DeleteTableAsync(ModelLibrary.Table table) {
            return base.Channel.DeleteTableAsync(table);
        }
    }
}
