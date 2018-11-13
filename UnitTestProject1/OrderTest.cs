using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using ControllerLibrary;
using DatabaseAccessLibrary;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Item = ModelLibrary.Item;
using Order = ModelLibrary.Order;
using OrderLineItem = ModelLibrary.OrderLineItem;

namespace UnitTests
{
    [TestClass]
    public class OrderTest5
    {
        [TestMethod]
        [DataRow("1000000", "1000000", "1000000", "2018-11-08 12:22:33", "2018-11-08 18:00:00", "2", "200.00", true, true, DisplayName = "Valid Input: Order Accepted")]
        [DataRow("1000000", "1000000", "1000000", "2018-11-08 12:22:33", "2018-11-08 18:00:00", "2", "200.00", false, true, DisplayName = "Valid Input: Order not Accepted")]
        [DataRow("100000", "1000000", "1000000", "2018-11-08 12:22:33", "2018-11-08 18:00:00", "2", "200.00", true, false, DisplayName = "Invalid ID length")]
        [DataRow("1000000", "1000000", "1000000", "2018-11-08", "2018-11-08 18:00:00", "2", "200.00", true, false, DisplayName = "Invalid dateTime")]
        [DataRow("1000000", "1000000", "1000000", "2018-11-08 12:22:33", "2018-11-818:00:00", "2", "200.00", true, false, DisplayName = "Invalid reservationDateTime")]
        [DataRow("1000000", "1000000", "1000000", "2018-11-08 12:22:33", "2018-11-08 18:00:00", "-1", "200.00", true, false, DisplayName = "Invalid noSeats")]
        public void Place_Order_Test(string orderId, string customerId, string restaurantId, string dateTime, string reservationDateTime,
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
            List<OrderLineItem> oli = new List<OrderLineItem>
            {
                //new OrderLineItem( new Item("Chicken Curry Soup", "Spicy curry soup with chicken breast and vegetables", 100.00), 2)
            };

            var ordCtrl = new OrderCtrl();
            //var resOrd = ordCtrl.CreateOrder("1000000", "1000000", "1000000", "2018-11-08 12:22:33", "2018-11-08 18:00:00", oli, "2", "200.00");
        }

        [TestMethod]
        public void Add_Order_To_Database()
        {
            List<OrderLineItem> oli = new List<OrderLineItem>
            {
                //new OrderLineItem( new Item("Chicken Curry Soup", "Spicy curry soup with chicken breast and vegetables", 100.00), 2)
            };

            var ordCtrl = new OrderCtrl();
            var ordDb = new OrderDb();
            var order = new ModelLibrary.Order
            {
                OrderId = "1000001",
                CustomerId = "1000000",
                RestaurantId = "1000000",
                DateTime = "2018-11-08 12:22:33",
                ReservationDateTime = "2018-11-08 18:00:00",
                ItemsList = oli,
                NoSeats = "2",
                Payment = "200.00"
            };

            ordCtrl.ConvertOrder(order);
        }
    }
}