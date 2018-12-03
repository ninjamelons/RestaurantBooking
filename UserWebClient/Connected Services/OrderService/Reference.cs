﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UserWebClient.OrderService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="OrderService.IOrderService")]
    public interface IOrderService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/AddItemToOrder", ReplyAction="http://tempuri.org/IOrderService/AddItemToOrderResponse")]
        void AddItemToOrder(int orderId, int itemId, int resId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/AddItemToOrder", ReplyAction="http://tempuri.org/IOrderService/AddItemToOrderResponse")]
        System.Threading.Tasks.Task AddItemToOrderAsync(int orderId, int itemId, int resId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/CreateOrder", ReplyAction="http://tempuri.org/IOrderService/CreateOrderResponse")]
        void CreateOrder(DatabaseAccessLibrary.Order order);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/CreateOrder", ReplyAction="http://tempuri.org/IOrderService/CreateOrderResponse")]
        System.Threading.Tasks.Task CreateOrderAsync(DatabaseAccessLibrary.Order order);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/GetOrderById", ReplyAction="http://tempuri.org/IOrderService/GetOrderByIdResponse")]
        DatabaseAccessLibrary.Order GetOrderById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/GetOrderById", ReplyAction="http://tempuri.org/IOrderService/GetOrderByIdResponse")]
        System.Threading.Tasks.Task<DatabaseAccessLibrary.Order> GetOrderByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/UpdateOrder", ReplyAction="http://tempuri.org/IOrderService/UpdateOrderResponse")]
        void UpdateOrder(DatabaseAccessLibrary.Order order);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/UpdateOrder", ReplyAction="http://tempuri.org/IOrderService/UpdateOrderResponse")]
        System.Threading.Tasks.Task UpdateOrderAsync(DatabaseAccessLibrary.Order order);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOrderServiceChannel : UserWebClient.OrderService.IOrderService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrderServiceClient : System.ServiceModel.ClientBase<UserWebClient.OrderService.IOrderService>, UserWebClient.OrderService.IOrderService {
        
        public OrderServiceClient() {
        }
        
        public OrderServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OrderServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void AddItemToOrder(int orderId, int itemId, int resId) {
            base.Channel.AddItemToOrder(orderId, itemId, resId);
        }
        
        public System.Threading.Tasks.Task AddItemToOrderAsync(int orderId, int itemId, int resId) {
            return base.Channel.AddItemToOrderAsync(orderId, itemId, resId);
        }
        
        public void CreateOrder(DatabaseAccessLibrary.Order order) {
            base.Channel.CreateOrder(order);
        }
        
        public System.Threading.Tasks.Task CreateOrderAsync(DatabaseAccessLibrary.Order order) {
            return base.Channel.CreateOrderAsync(order);
        }
        
        public DatabaseAccessLibrary.Order GetOrderById(int id) {
            return base.Channel.GetOrderById(id);
        }
        
        public System.Threading.Tasks.Task<DatabaseAccessLibrary.Order> GetOrderByIdAsync(int id) {
            return base.Channel.GetOrderByIdAsync(id);
        }
        
        public void UpdateOrder(DatabaseAccessLibrary.Order order) {
            base.Channel.UpdateOrder(order);
        }
        
        public System.Threading.Tasks.Task UpdateOrderAsync(DatabaseAccessLibrary.Order order) {
            return base.Channel.UpdateOrderAsync(order);
        }
    }
}
