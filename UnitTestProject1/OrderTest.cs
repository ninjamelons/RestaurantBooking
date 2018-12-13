using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ControllerLibrary;
using DatabaseAccessLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Order = ModelLibrary.Order;
using OrderLineItem = ModelLibrary.OrderLineItem;

namespace UnitTests
{
    [TestClass]
    public class OrderTest
    {
//        private readonly CustomerService

        [TestInitialize]
        public void TestInitialize()
        {
            var resetDb = new ResetDb();
            resetDb.Clean();
        }

        [TestMethod]
        [DataRow("1000000", "1000000", "1000000", "2018-11-08 12:22:33", "2018-11-08 18:00:00", "2", "200.00", true,
            true, DisplayName = "Valid Input: Order Accepted")]
        [DataRow("1000000", "1000000", "1000000", "2018-11-08 12:22:33", "2018-11-08 18:00:00", "2", "200.00", false,
            true, DisplayName = "Valid Input: Order not Accepted")]
        [DataRow("100000", "1000000", "1000000", "2018-11-08 12:22:33", "2018-11-08 18:00:00", "2", "200.00", true,
            false, DisplayName = "Invalid ID length")]
        [DataRow("1000000", "1000000", "1000000", "2018-11-08", "2018-11-08 18:00:00", "2", "200.00", true, false,
            DisplayName = "Invalid dateTime")]
        [DataRow("1000000", "1000000", "1000000", "2018-11-08 12:22:33", "2018-11-818:00:00", "2", "200.00", true,
            false, DisplayName = "Invalid reservationDateTime")]
        [DataRow("1000000", "1000000", "1000000", "2018-11-08 12:22:33", "2018-11-08 18:00:00", "-1", "200.00", true,
            false, DisplayName = "Invalid noSeats")]
        public void Place_Order_Test(string orderId, string customerId, string restaurantId, string dateTime,
            string reservationDateTime,
            string noSeats, string payment, bool accepted, bool shouldValidate)
        {
            //Setup
            var itemList = new List<OrderLineItem>();
            var sut = new Order
            {
                OrderId = orderId,
                CustomerId = customerId,
                RestaurantId = restaurantId,
                DateTime = dateTime,
                ReservationDateTime = reservationDateTime,
                ItemsList = itemList,
                NoSeats = noSeats,
                Payment = payment,
                Accepted = accepted
            };

            //itemList.Add(new Item("Chicken Curry Soup", "Spicy curry soup with chicken breast and vegetables", 100.00, 2));

            var context = new ValidationContext(sut, null, null);
            var result = new List<ValidationResult>();

            //Act
            var isModelStateValid = Validator.TryValidateObject(sut, context, result, true);

            //Assert
            Assert.IsTrue(shouldValidate == isModelStateValid);
        }

        [TestMethod]
        public void Get_OrderHistory_By_CustomerId_Test()
        {
            //Setup
            var cusC = new CustomerCtrl();

            //Act
            var orderHistory = cusC.GetOrdersByCustomerId(1000000);

            //Assert
            Assert.IsTrue(orderHistory[0].id == 1000000 && orderHistory[0].OrderLineItems[0].itemId == 1000000);
        }

        [TestMethod]
        public void Convert_To_Database_Test()
        {
            //Setup
            var oli = new List<OrderLineItem>();
            var ordCtrl = new OrderCtrl();
            var ordDb = new Order();
            var order = new Order
            {
                OrderId = "1000002",
                CustomerId = "1000000",
                RestaurantId = "1000000",
                DateTime = "2018-11-08 12:22:33",
                ReservationDateTime = "2018-11-08 18:00:00",
                ItemsList = oli,
                NoSeats = "2",
                Payment = "200.00",
                Accepted = false
            };

            var dt = new DateTime(2018, 11, 08, 12, 22, 33);
            var resDt = new DateTime(2018, 11, 08, 18, 00, 00);

            var dbOrder = new DatabaseAccessLibrary.Order
            {
                id = 1000002,
                restaurantId = 1000000,
                dateTime = dt,
                reservation = resDt,
                noSeats = 2
            };

            //Act
            var resOrder = ordCtrl.ConvertOrder(order);

            //Assert
            Assert.IsTrue(resOrder.id == dbOrder.id &&
                          resOrder.restaurantId == dbOrder.restaurantId &&
                          resOrder.dateTime == dbOrder.dateTime &&
                          resOrder.reservation == dbOrder.reservation &&
                          resOrder.noSeats == dbOrder.noSeats);

        }

        [TestMethod]
        public void Add_Order_To_Database_Test()
        {
            //Setup
            var ordCtrl = new OrderCtrl();
            var ordDb = new OrderDb();
            var dt = new DateTime(2018, 11, 08, 12, 22, 33);
            var resDt = new DateTime(2018, 11, 08, 18, 00, 00);
            var oli = new List<OrderLineItem>();
            var item = new ModelLibrary.Item();
            item.Id = 1000000;
            oli.Add(new OrderLineItem(item, 2));

            var order = new Order
            {
                OrderId = (ordCtrl.GetLastOrderIdentity() + 1).ToString(),
                CustomerId = "1000000",
                RestaurantId = "1000000",
                DateTime = "2018-11-08 12:22:33",
                ReservationDateTime = "2018-11-08 18:00:00",
                ItemsList = oli,
                NoSeats = "2",
                Payment = "200.00",
                Accepted = false
            };

            //Act
            var oliList = ordCtrl.ConvertOrderLineItemsToDb(order);
            var dbOrder = ordCtrl.ConvertOrder(order);
            for (var i = 0; i < dbOrder.OrderLineItems.Count - 1; i++)
            {
                dbOrder.OrderLineItems.Add(oliList[i]);
            }
            ordDb.AddOrder(dbOrder);

            //Assert
            Assert.IsTrue(ordCtrl.GetLastOrderIdentity() == Convert.ToInt32(order.OrderId));
        }

        [TestMethod]
        public void Add_Item_To_Order_Increment_Quantity_Test()
        {
            //Setup
            var ordC = new OrderCtrl();

            //Act
            ordC.AddItemToOrder(1000000, 1000000);

            //Assert
            Assert.IsTrue(ordC.GetOrderById(1000000).ItemsList[0].Quantity == 2);
        }

        [TestMethod]
        public void Add_New_Item_To_Order_Test()
        {
            //Setup
            var ordC = new OrderCtrl();

            //Act
            ordC.AddItemToOrder(1000000, 1000001);

            //Assert
            Assert.IsTrue(ordC.GetOrderById(1000000).ItemsList[1].LineItem.Id == 1000001);
        }

        [TestMethod]
        public void Get_Order_By_ID_Test()
        {
            //Setup
            var ordC = new OrderCtrl();

            //Act
            var dbO = ordC.GetOrderById(1000000);

            //Assert
            Assert.IsTrue(dbO.OrderId == "1000000");
        }

        [TestMethod]
        public void Update_Order_Test()
        {
            //Setup
            var ordC = new OrderCtrl(); var ordDb = new Order();
            var dt = new DateTime(2018, 11, 08, 12, 22, 33);
            var newResDt = new DateTime(2018, 11, 08, 19, 00, 00);

            var dbOrder = new DatabaseAccessLibrary.Order
            {
                id = 1000000,
                restaurantId = 1000000,
                dateTime = dt,
                reservation = newResDt,
                noSeats = 2,
                accepted = false
            };

            //Act
            ordC.UpdateOrder(dbOrder);

            //Assert
            Assert.IsTrue(ordC.GetOrderById(1000000).ReservationDateTime == newResDt.ToString());
        }

        [TestMethod]
        public void Get_Last_Identity_Test()
        {
            //Setup
            var ordC = new OrderCtrl();

            //Assert
            Assert.IsTrue(ordC.GetLastOrderIdentity() == 1000005);
        }

        [TestMethod]
        public void Convert_OrderLineItem_To_Db_Test()
        {
            //Setup
            var ordC = new OrderCtrl();
            var oli = new List<OrderLineItem>();
            var item = new ModelLibrary.Item();
            item.Id = 1000000;
            oli.Add(new OrderLineItem(item, 2));
            var mOrder = new Order
            {
                OrderId = "1000002",
                CustomerId = "1000000",
                RestaurantId = "1000000",
                DateTime = "2018-11-08 12:22:33",
                ReservationDateTime = "2018-11-08 18:00:00",
                ItemsList = oli,
                NoSeats = "2",
                Payment = "200.00",
                Accepted = false
            };

            //Act
            var dbOrd = ordC.ConvertOrderLineItemsToDb(mOrder);

            //Assert
            Assert.IsTrue(dbOrd[0].itemId == 1000000 &&
                          dbOrd[0].orderId == 1000002);
        }
    }
}