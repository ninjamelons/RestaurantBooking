using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using ControllerLibrary;
using DatabaseAccessLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;
using Order = ModelLibrary.Order;
using OrderLineItem = ModelLibrary.OrderLineItem;

namespace UnitTests
{
    [TestClass]
    public class OrderTest5
    {
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

            var isModelStateValid = Validator.TryValidateObject(sut, context, result, true);

            Assert.IsTrue(shouldValidate == isModelStateValid);
        }

        [TestMethod]
        public void Order_Creation_Test()
        {
            var oli = new List<OrderLineItem>();

            var ordCtrl = new OrderCtrl();
            //var resOrd = ordCtrl.CreateOrder("1000000", "1000000", "1000000", "2018-11-08 12:22:33", "2018-11-08 18:00:00", oli, "2", "200.00");
        }

        [TestMethod]
        public void Convert_To_Database_Test()
        {
            var oli = new List<OrderLineItem>();
            var ordCtrl = new OrderCtrl();
            var ordDb = new OrderDb();
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
            var resOrder = ordCtrl.ConvertOrder(order);
            Assert.IsNotNull(resOrder.id);
            Assert.IsTrue(resOrder.id == dbOrder.id &&
                          resOrder.restaurantId == dbOrder.restaurantId &&
                          resOrder.dateTime == dbOrder.dateTime &&
                          resOrder.reservation == dbOrder.reservation &&
                          resOrder.noSeats == dbOrder.noSeats);

        }

        [TestMethod]
        public void Add_Order_To_Database_Test()
        {
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
            var oliList = ordCtrl.ConvertOrderLineItemsToDb(order);
            var dbOrder = ordCtrl.ConvertOrder(order);
            for (int i = 0; i < dbOrder.OrderLineItems.Count - 1; i++)
            {
                dbOrder.OrderLineItems.Add(oliList[i]);
            }
            ordDb.AddOrder(dbOrder);
            Assert.IsTrue(ordCtrl.GetLastOrderIdentity() == Convert.ToInt32(order.OrderId));
        }

        [TestMethod]
        public void Add_Item_To_Order_Increment_Quantity_Test()
        {
            var ordC = new OrderCtrl();
            ordC.AddItemToOrder(1000000, 1000000);
            Assert.IsTrue(ordC.GetOrderById(1000000).OrderLineItems[0].quantity == 3);
        }

        [TestMethod]
        public void Add_New_Item_To_Order_Test()
        {
            var ordC = new OrderCtrl();
            ordC.AddItemToOrder(1000000, 1000001);
            Assert.IsTrue(ordC.GetOrderById(1000000).OrderLineItems[1].itemId == 1000001);
        }

        [TestMethod]
        public void Get_Order_By_ID_Test()
        {
            var ordC = new OrderCtrl();
            var dbO = ordC.GetOrderById(1000000);

            Assert.IsTrue(dbO.id == 1000000);
        }

        [TestMethod]
        public void Update_Order_Test()
        {
            var ordC = new OrderCtrl(); var ordDb = new OrderDb();
            var dt = new DateTime(2018, 11, 08, 12, 22, 33);
            var newResDt = new DateTime(2018, 11, 08, 19, 00, 00);

            var dbOrder = new DatabaseAccessLibrary.Order
            {
                restaurantId = 1000000,
                dateTime = dt,
                reservation = newResDt,
                noSeats = 2,
                accepted = false
            };
            ordC.UpdateOrder(dbOrder);
            Assert.IsTrue(ordC.GetOrderById(1000001).reservation == newResDt);
        }

        [TestMethod]
        public void Get_Last_Identity_Test()
        {
            OrderCtrl ordC = new OrderCtrl();

            Assert.IsTrue(ordC.GetLastOrderIdentity() == 1000001);
        }

        [TestMethod]
        public void Convert_OrderLineItem_To_Db_Test()
        {
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
            var dbOrd = ordC.ConvertOrderLineItemsToDb(mOrder);

            Assert.IsTrue(dbOrd[0].itemId == 1000000 &&
                          dbOrd[0].orderId == 1000002);
        }
    }
}