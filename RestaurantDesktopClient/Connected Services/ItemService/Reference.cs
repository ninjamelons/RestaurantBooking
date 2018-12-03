﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestaurantDesktopClient.ItemService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ItemService.IItemService")]
    public interface IItemService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetItemCatById", ReplyAction="http://tempuri.org/IItemService/GetItemCatByIdResponse")]
        ModelLibrary.ItemCat GetItemCatById(int itemCatId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetItemCatById", ReplyAction="http://tempuri.org/IItemService/GetItemCatByIdResponse")]
        System.Threading.Tasks.Task<ModelLibrary.ItemCat> GetItemCatByIdAsync(int itemCatId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetItemCatByName", ReplyAction="http://tempuri.org/IItemService/GetItemCatByNameResponse")]
        ModelLibrary.ItemCat GetItemCatByName(string itemCatName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetItemCatByName", ReplyAction="http://tempuri.org/IItemService/GetItemCatByNameResponse")]
        System.Threading.Tasks.Task<ModelLibrary.ItemCat> GetItemCatByNameAsync(string itemCatName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetItem", ReplyAction="http://tempuri.org/IItemService/GetItemResponse")]
        ModelLibrary.Item GetItem(int itemId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetItem", ReplyAction="http://tempuri.org/IItemService/GetItemResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Item> GetItemAsync(int itemId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetItemByNameAndMenuId", ReplyAction="http://tempuri.org/IItemService/GetItemByNameAndMenuIdResponse")]
        ModelLibrary.Item GetItemByNameAndMenuId(string itemName, int menuId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetItemByNameAndMenuId", ReplyAction="http://tempuri.org/IItemService/GetItemByNameAndMenuIdResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Item> GetItemByNameAndMenuIdAsync(string itemName, int menuId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetItemByName", ReplyAction="http://tempuri.org/IItemService/GetItemByNameResponse")]
        ModelLibrary.Item GetItemByName(string itemName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetItemByName", ReplyAction="http://tempuri.org/IItemService/GetItemByNameResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Item> GetItemByNameAsync(string itemName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetItemPrice", ReplyAction="http://tempuri.org/IItemService/GetItemPriceResponse")]
        ModelLibrary.Price GetItemPrice(ModelLibrary.Item item, int menuId, int itemCatId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetItemPrice", ReplyAction="http://tempuri.org/IItemService/GetItemPriceResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Price> GetItemPriceAsync(ModelLibrary.Item item, int menuId, int itemCatId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/CreateItem", ReplyAction="http://tempuri.org/IItemService/CreateItemResponse")]
        void CreateItem(ModelLibrary.Item item, int menuId, int itemCatId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/CreateItem", ReplyAction="http://tempuri.org/IItemService/CreateItemResponse")]
        System.Threading.Tasks.Task CreateItemAsync(ModelLibrary.Item item, int menuId, int itemCatId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/UpdateItem", ReplyAction="http://tempuri.org/IItemService/UpdateItemResponse")]
        void UpdateItem(ModelLibrary.Item updatedItem, int menuId, int itemCatId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/UpdateItem", ReplyAction="http://tempuri.org/IItemService/UpdateItemResponse")]
        System.Threading.Tasks.Task UpdateItemAsync(ModelLibrary.Item updatedItem, int menuId, int itemCatId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/DeleteItem", ReplyAction="http://tempuri.org/IItemService/DeleteItemResponse")]
        void DeleteItem(int itemId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/DeleteItem", ReplyAction="http://tempuri.org/IItemService/DeleteItemResponse")]
        System.Threading.Tasks.Task DeleteItemAsync(int itemId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetAllItemsByCategory", ReplyAction="http://tempuri.org/IItemService/GetAllItemsByCategoryResponse")]
        ModelLibrary.Item[] GetAllItemsByCategory(int categoryId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetAllItemsByCategory", ReplyAction="http://tempuri.org/IItemService/GetAllItemsByCategoryResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Item[]> GetAllItemsByCategoryAsync(int categoryId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetAllItemsByMenu", ReplyAction="http://tempuri.org/IItemService/GetAllItemsByMenuResponse")]
        ModelLibrary.Item[] GetAllItemsByMenu(int menuId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetAllItemsByMenu", ReplyAction="http://tempuri.org/IItemService/GetAllItemsByMenuResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Item[]> GetAllItemsByMenuAsync(int menuId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetAllItemsByRestaurant", ReplyAction="http://tempuri.org/IItemService/GetAllItemsByRestaurantResponse")]
        ModelLibrary.Item[] GetAllItemsByRestaurant(int restaurandId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetAllItemsByRestaurant", ReplyAction="http://tempuri.org/IItemService/GetAllItemsByRestaurantResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Item[]> GetAllItemsByRestaurantAsync(int restaurandId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/CreateItemCat", ReplyAction="http://tempuri.org/IItemService/CreateItemCatResponse")]
        void CreateItemCat(ModelLibrary.ItemCat itemCat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/CreateItemCat", ReplyAction="http://tempuri.org/IItemService/CreateItemCatResponse")]
        System.Threading.Tasks.Task CreateItemCatAsync(ModelLibrary.ItemCat itemCat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/UpdateItemCat", ReplyAction="http://tempuri.org/IItemService/UpdateItemCatResponse")]
        void UpdateItemCat(ModelLibrary.ItemCat beforeItemCat, ModelLibrary.ItemCat afterItemCat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/UpdateItemCat", ReplyAction="http://tempuri.org/IItemService/UpdateItemCatResponse")]
        System.Threading.Tasks.Task UpdateItemCatAsync(ModelLibrary.ItemCat beforeItemCat, ModelLibrary.ItemCat afterItemCat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/DeleteItemCat", ReplyAction="http://tempuri.org/IItemService/DeleteItemCatResponse")]
        void DeleteItemCat(int itemCatId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/DeleteItemCat", ReplyAction="http://tempuri.org/IItemService/DeleteItemCatResponse")]
        System.Threading.Tasks.Task DeleteItemCatAsync(int itemCatId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetAllItemCategories", ReplyAction="http://tempuri.org/IItemService/GetAllItemCategoriesResponse")]
        ModelLibrary.ItemCat[] GetAllItemCategories();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetAllItemCategories", ReplyAction="http://tempuri.org/IItemService/GetAllItemCategoriesResponse")]
        System.Threading.Tasks.Task<ModelLibrary.ItemCat[]> GetAllItemCategoriesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetItemCats", ReplyAction="http://tempuri.org/IItemService/GetItemCatsResponse")]
        ModelLibrary.ItemCat[] GetItemCats();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetItemCats", ReplyAction="http://tempuri.org/IItemService/GetItemCatsResponse")]
        System.Threading.Tasks.Task<ModelLibrary.ItemCat[]> GetItemCatsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetCatByItemCatId", ReplyAction="http://tempuri.org/IItemService/GetCatByItemCatIdResponse")]
        ModelLibrary.ItemCat GetCatByItemCatId(int itemId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItemService/GetCatByItemCatId", ReplyAction="http://tempuri.org/IItemService/GetCatByItemCatIdResponse")]
        System.Threading.Tasks.Task<ModelLibrary.ItemCat> GetCatByItemCatIdAsync(int itemId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IItemServiceChannel : RestaurantDesktopClient.ItemService.IItemService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ItemServiceClient : System.ServiceModel.ClientBase<RestaurantDesktopClient.ItemService.IItemService>, RestaurantDesktopClient.ItemService.IItemService {
        
        public ItemServiceClient() {
        }
        
        public ItemServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ItemServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ItemServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ItemServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ModelLibrary.ItemCat GetItemCatById(int itemCatId) {
            return base.Channel.GetItemCatById(itemCatId);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.ItemCat> GetItemCatByIdAsync(int itemCatId) {
            return base.Channel.GetItemCatByIdAsync(itemCatId);
        }
        
        public ModelLibrary.ItemCat GetItemCatByName(string itemCatName) {
            return base.Channel.GetItemCatByName(itemCatName);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.ItemCat> GetItemCatByNameAsync(string itemCatName) {
            return base.Channel.GetItemCatByNameAsync(itemCatName);
        }
        
        public ModelLibrary.Item GetItem(int itemId) {
            return base.Channel.GetItem(itemId);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Item> GetItemAsync(int itemId) {
            return base.Channel.GetItemAsync(itemId);
        }
        
        public ModelLibrary.Item GetItemByNameAndMenuId(string itemName, int menuId) {
            return base.Channel.GetItemByNameAndMenuId(itemName, menuId);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Item> GetItemByNameAndMenuIdAsync(string itemName, int menuId) {
            return base.Channel.GetItemByNameAndMenuIdAsync(itemName, menuId);
        }
        
        public ModelLibrary.Item GetItemByName(string itemName) {
            return base.Channel.GetItemByName(itemName);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Item> GetItemByNameAsync(string itemName) {
            return base.Channel.GetItemByNameAsync(itemName);
        }
        
        public ModelLibrary.Price GetItemPrice(ModelLibrary.Item item, int menuId, int itemCatId) {
            return base.Channel.GetItemPrice(item, menuId, itemCatId);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Price> GetItemPriceAsync(ModelLibrary.Item item, int menuId, int itemCatId) {
            return base.Channel.GetItemPriceAsync(item, menuId, itemCatId);
        }
        
        public void CreateItem(ModelLibrary.Item item, int menuId, int itemCatId) {
            base.Channel.CreateItem(item, menuId, itemCatId);
        }
        
        public System.Threading.Tasks.Task CreateItemAsync(ModelLibrary.Item item, int menuId, int itemCatId) {
            return base.Channel.CreateItemAsync(item, menuId, itemCatId);
        }
        
        public void UpdateItem(ModelLibrary.Item updatedItem, int menuId, int itemCatId) {
            base.Channel.UpdateItem(updatedItem, menuId, itemCatId);
        }
        
        public System.Threading.Tasks.Task UpdateItemAsync(ModelLibrary.Item updatedItem, int menuId, int itemCatId) {
            return base.Channel.UpdateItemAsync(updatedItem, menuId, itemCatId);
        }
        
        public void DeleteItem(int itemId) {
            base.Channel.DeleteItem(itemId);
        }
        
        public System.Threading.Tasks.Task DeleteItemAsync(int itemId) {
            return base.Channel.DeleteItemAsync(itemId);
        }
        
        public ModelLibrary.Item[] GetAllItemsByCategory(int categoryId) {
            return base.Channel.GetAllItemsByCategory(categoryId);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Item[]> GetAllItemsByCategoryAsync(int categoryId) {
            return base.Channel.GetAllItemsByCategoryAsync(categoryId);
        }
        
        public ModelLibrary.Item[] GetAllItemsByMenu(int menuId) {
            return base.Channel.GetAllItemsByMenu(menuId);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Item[]> GetAllItemsByMenuAsync(int menuId) {
            return base.Channel.GetAllItemsByMenuAsync(menuId);
        }
        
        public ModelLibrary.Item[] GetAllItemsByRestaurant(int restaurandId) {
            return base.Channel.GetAllItemsByRestaurant(restaurandId);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Item[]> GetAllItemsByRestaurantAsync(int restaurandId) {
            return base.Channel.GetAllItemsByRestaurantAsync(restaurandId);
        }
        
        public void CreateItemCat(ModelLibrary.ItemCat itemCat) {
            base.Channel.CreateItemCat(itemCat);
        }
        
        public System.Threading.Tasks.Task CreateItemCatAsync(ModelLibrary.ItemCat itemCat) {
            return base.Channel.CreateItemCatAsync(itemCat);
        }
        
        public void UpdateItemCat(ModelLibrary.ItemCat beforeItemCat, ModelLibrary.ItemCat afterItemCat) {
            base.Channel.UpdateItemCat(beforeItemCat, afterItemCat);
        }
        
        public System.Threading.Tasks.Task UpdateItemCatAsync(ModelLibrary.ItemCat beforeItemCat, ModelLibrary.ItemCat afterItemCat) {
            return base.Channel.UpdateItemCatAsync(beforeItemCat, afterItemCat);
        }
        
        public void DeleteItemCat(int itemCatId) {
            base.Channel.DeleteItemCat(itemCatId);
        }
        
        public System.Threading.Tasks.Task DeleteItemCatAsync(int itemCatId) {
            return base.Channel.DeleteItemCatAsync(itemCatId);
        }
        
        public ModelLibrary.ItemCat[] GetAllItemCategories() {
            return base.Channel.GetAllItemCategories();
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.ItemCat[]> GetAllItemCategoriesAsync() {
            return base.Channel.GetAllItemCategoriesAsync();
        }
        
        public ModelLibrary.ItemCat[] GetItemCats() {
            return base.Channel.GetItemCats();
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.ItemCat[]> GetItemCatsAsync() {
            return base.Channel.GetItemCatsAsync();
        }
        
        public ModelLibrary.ItemCat GetCatByItemCatId(int itemId) {
            return base.Channel.GetCatByItemCatId(itemId);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.ItemCat> GetCatByItemCatIdAsync(int itemId) {
            return base.Channel.GetCatByItemCatIdAsync(itemId);
        }
    }
}