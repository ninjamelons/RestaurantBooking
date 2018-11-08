using System;
using System.Collections.Generic;
using Moq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using DatabaseAccessLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;
using RestaurantBookingService.Interfaces;

namespace UnitTestProject1
{
    [TestClass]
    public class CRUDTablesTest
    {
        [TestMethod]
        [DataRow("4","20","0",true,DisplayName = "All valid")]
        [DataRow("4","20","30",false,DisplayName = "Reserved tables > Total tables")]
        [DataRow("4","20","-15",false,DisplayName = "Reserved negative")]
        [DataRow("4","20","1t0",false,DisplayName = "Reserved has letter")]
        [DataRow("4","20","1£0",false,DisplayName = "Reserved has symbol")]
        [DataRow("4","20","   0  ",false,DisplayName = "Reserved has space")]
        [DataRow("4","-20","0",false,DisplayName = "Total tables negative")]
        [DataRow("4","2t0","0",false,DisplayName = "Total tables has letter")]
        [DataRow("4","2£0","0",false,DisplayName = "Total tables has symbol")]
        [DataRow("4","    2 0  ","0",false,DisplayName = "Total has space")]
        [DataRow("-4","20","0",false,DisplayName = "NoSeats negative")]
        [DataRow("4t","20","0",false,DisplayName = "NoSeats has letter")]
        [DataRow("4$","20","0",false,DisplayName = "NoSeats has symbol")]
        [DataRow(" 4  ","20","0",false,DisplayName = "NoSeats has space")]
        public void Create_Table_Valid_Inputs(string noSeats, string total, string reserved, bool shouldValidate)
        {
            //Setup
            var sut = new Table
            {
                NoSeats = noSeats,
                Total = total,
                Reserved = reserved
            };

            var context = new ValidationContext(sut, null, null);
            var result = new List<ValidationResult>();

            //Act
            var isModelStateValid = Validator.TryValidateObject(sut, context, result, true);

            //Assert
            Assert.IsTrue(isModelStateValid == shouldValidate);
        }

        [TestMethod]
        public void Create_Table_Added_To_Db()
        {
            //Setup
            ResTable newTable = new ResTable
            {
                noSeats = 4, reserved = 0, restaurantId = 1, total = 20
            };
            Restaurant newRestaurant = new Restaurant
            {
                address = "hobro", email = "ucn@ucn.dk", id = 1
            };

            RestaurantsDb ResDb = new RestaurantsDb();

            //Act
            ResDb.AddTable(newTable, newRestaurant);

            //Get table with: noSeats = 4, reserved = 0, restaurantId = 1, total = 20
            ResTable resTable = ResDb.GetTable(4,0,1,20);

            //Assert
            Assert.Equals(resTable, newTable);
        }

        [TestMethod]
        public void Create_Table_ModelLayer_To_DbModel()
        {
            //Setup
            Table table = new Table
            {
                NoSeats = "4", Reserved = "0", Total = "20"
            };
            ResTable newTable = new ResTable
            {
                noSeats = 4, reserved = 0, restaurantId = 1, total = 20
            };

            //Act
            ResTable resTable = ControllerLibrary.ConvertTable(table);

            //Assert
            Assert.Equals(resTable, newTable);
        }
    }
}
