using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Order = ModelLibrary.Order;

namespace UnitTestProject1
{
    [TestClass]
    public class OrderTest
    {
        [TestMethod]
        [DataRow("1000000", "2018-11-08 12:22:33", "2018-11-08 18:00:00", "2", true, true, DisplayName = "Valid input")]
        [DataRow("100000", "2018-11-08 12:22:33", "2018-11-08 18:00:00", "2", true, false, DisplayName = "Invalid ID length")]
        [DataRow("1000000", "2018-11-08", "2018-11-08 18:00:00", "2", true, false, DisplayName = "Invalid dateTime")]
        [DataRow("1000000", "2018-11-08 12:22:33", "2018-11-818:00:00", "2", true, false, DisplayName = "Invalid reservationDateTime")]
        [DataRow("1000000", "2018-11-08 12:22:33", "2018-11-08 18:00:00", "-1", true, false, DisplayName = "Invalid noSeats")]
        [DataRow("1000000", "2018-11-08 12:22:33", "2018-11-08 18:00:00", "2", "true", true, DisplayName = "Invalid bool")]
        [DataRow("100000", "2018-11-08 12:22:33", "2018-11-08 18:00:00", "2", false, true, DisplayName = "Invalid ID length")]
        public void Place_Order_Test(string orderId, string dateTime, string reservationDateTime,
            string noSeats, bool accepted, bool shouldValidate)
        {
            Mock itemMock = new Mock<Item>();
            List<Item> itemList = new List<Item>
            {
                new Mock<Item>("1000000", "Chicken Curry Soup", "Spicy curry soup with chicken breast and vegetables", "1000000").Object
            };
            var sut = new Order
            {
                OrderId = orderId,
                DateTime = dateTime,
                ReservationDateTime = reservationDateTime,
                ItemsList = itemList,
                NoSeats = noSeats,
                Accepted = accepted
            };

            var context = new ValidationContext(sut, null, null);
            var result = new List<ValidationResult>();

            var isModelStateValid = Validator.TryValidateObject(sut, context, result, true);

            Assert.IsTrue(shouldValidate == isModelStateValid);
        }
    }
}