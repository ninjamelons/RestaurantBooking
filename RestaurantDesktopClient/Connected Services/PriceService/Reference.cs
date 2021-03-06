﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestaurantDesktopClient.PriceService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PriceService.IPriceService")]
    public interface IPriceService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPriceService/CreatePrice", ReplyAction="http://tempuri.org/IPriceService/CreatePriceResponse")]
        void CreatePrice(ModelLibrary.Price price, int itemId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPriceService/CreatePrice", ReplyAction="http://tempuri.org/IPriceService/CreatePriceResponse")]
        System.Threading.Tasks.Task CreatePriceAsync(ModelLibrary.Price price, int itemId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPriceService/DeletePricesByItemId", ReplyAction="http://tempuri.org/IPriceService/DeletePricesByItemIdResponse")]
        void DeletePricesByItemId(int itemId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPriceService/DeletePricesByItemId", ReplyAction="http://tempuri.org/IPriceService/DeletePricesByItemIdResponse")]
        System.Threading.Tasks.Task DeletePricesByItemIdAsync(int itemId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPriceService/GetLatestPrice", ReplyAction="http://tempuri.org/IPriceService/GetLatestPriceResponse")]
        ModelLibrary.Price GetLatestPrice(int itemId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPriceService/GetLatestPrice", ReplyAction="http://tempuri.org/IPriceService/GetLatestPriceResponse")]
        System.Threading.Tasks.Task<ModelLibrary.Price> GetLatestPriceAsync(int itemId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPriceService/UpdatePrice", ReplyAction="http://tempuri.org/IPriceService/UpdatePriceResponse")]
        void UpdatePrice(ModelLibrary.Price price, int itemId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPriceService/UpdatePrice", ReplyAction="http://tempuri.org/IPriceService/UpdatePriceResponse")]
        System.Threading.Tasks.Task UpdatePriceAsync(ModelLibrary.Price price, int itemId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPriceServiceChannel : RestaurantDesktopClient.PriceService.IPriceService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PriceServiceClient : System.ServiceModel.ClientBase<RestaurantDesktopClient.PriceService.IPriceService>, RestaurantDesktopClient.PriceService.IPriceService {
        
        public PriceServiceClient() {
        }
        
        public PriceServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PriceServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PriceServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PriceServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void CreatePrice(ModelLibrary.Price price, int itemId) {
            base.Channel.CreatePrice(price, itemId);
        }
        
        public System.Threading.Tasks.Task CreatePriceAsync(ModelLibrary.Price price, int itemId) {
            return base.Channel.CreatePriceAsync(price, itemId);
        }
        
        public void DeletePricesByItemId(int itemId) {
            base.Channel.DeletePricesByItemId(itemId);
        }
        
        public System.Threading.Tasks.Task DeletePricesByItemIdAsync(int itemId) {
            return base.Channel.DeletePricesByItemIdAsync(itemId);
        }
        
        public ModelLibrary.Price GetLatestPrice(int itemId) {
            return base.Channel.GetLatestPrice(itemId);
        }
        
        public System.Threading.Tasks.Task<ModelLibrary.Price> GetLatestPriceAsync(int itemId) {
            return base.Channel.GetLatestPriceAsync(itemId);
        }
        
        public void UpdatePrice(ModelLibrary.Price price, int itemId) {
            base.Channel.UpdatePrice(price, itemId);
        }
        
        public System.Threading.Tasks.Task UpdatePriceAsync(ModelLibrary.Price price, int itemId) {
            return base.Channel.UpdatePriceAsync(price, itemId);
        }
    }
}
