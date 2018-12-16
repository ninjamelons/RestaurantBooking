using System;
using System.Collections.Generic;
using ControllerLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;

namespace UnitTests
{
    [TestClass]
    public class ReserveTablesAlgorithm
    {
        
        [TestMethod]
        [DataRow(9)]
        [DataRow(10)]
        [DataRow(12)]
        [DataRow(14)]
        [DataRow(16)]
        public void Least_Number_Of_Tables_Per_Order_Test(int tempSeats)
        {
            var tableC = new TableCtrl();

            #region tablesList

            var id = 1000000;
            var tables = new List<Table>();
            tables.Add(new Table { NoSeats = 2, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 2, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 4, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 6, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 8, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 10, Reserved = false, RestaurantId = id });

            #endregion

            //Act
            var tablesUsed = tableC.LeastNumberOfTables(tables, tempSeats);

            //Assert
            Assert.IsTrue(3 > tablesUsed.Count);

        }
    }
}
