using System;
using System.Collections.Generic;
using ControllerLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;

namespace UnitTestProject1
{
    [TestClass]
    public class ReserveTablesAlgorithm
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        [DataRow(8)]
        [DataRow(9)]
        [DataRow(10)]
        public void GetAllModuloCount(int tempSeats)
        {
            //Setup: Needs tablesList, tempSeats no.
            var tblCtrl = new TableCtrl();
            #region tablesList

            var id = 1000000;
            List<Table> tables = new List<Table>();
            tables.Add(new Table { NoSeats = 4, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 4, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 2, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 2, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 5, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 5, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 5, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 6, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 6, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 6, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 7, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 6, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 8, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 8, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 9, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 9, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 10, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 9, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 9, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 10, Reserved = false, RestaurantId = id });

            #endregion
            
            //Act
            var moduloDict = tblCtrl.GetAllModulo(tables, tempSeats);

            //Assert
            Assert.AreEqual(20, moduloDict.Count);
        }
        
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

        [TestMethod]
        public void FindSmallestModuloCount()
        {
            //Setup
            var tblCtrl = new TableCtrl();
            var moduloDict = new Dictionary<int, int>();
            moduloDict.Add(0,5);
            moduloDict.Add(1,2);
            moduloDict.Add(2,1);
            moduloDict.Add(3,2);
            moduloDict.Add(4,2);
            moduloDict.Add(5,3);
            moduloDict.Add(6,5);
            moduloDict.Add(7,5);
            moduloDict.Add(8,1);
            moduloDict.Add(9,1);
            moduloDict.Add(10,6);
            moduloDict.Add(11,6);
            moduloDict.Add(12,7);
            moduloDict.Add(13,1);

            //Act
            var testDict = tblCtrl.FindSmallestModulo(moduloDict);

            //Assert
            Assert.AreEqual(4,testDict.Count,"4 key value pairs with a min value of 1");
        }

        [TestMethod]
        public void GetTablesSmallestModuloCount()
        {
            //Setup
            var tblCtrl = new TableCtrl();
            #region tablesList

            var id = 1000000;
            List<Table> tables = new List<Table>();
            tables.Add(new Table { NoSeats = 4, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 2, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 5, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 5, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 7, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 8, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 9, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 9, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 10, Reserved = false, RestaurantId = id });

            #endregion

            #region smallDict

            //TODO needs a lot of fixes still
            // 6 / 2 GIVES 0, a group of 6 people would be given 3 tables of 2 rather than a table of 5 and 2
            // Test data doesn't have 3 tables of 2, instead it gives table of 2 and 4 maximizing seat no.s
            // but not table sizes
            var tablesModulo = tblCtrl.GetAllModulo(tables, 6);
            var smallDict = tblCtrl.FindSmallestModulo(tablesModulo);

            #endregion

            //Act
            var tablesSmall = tblCtrl.GetTablesSmallestModulo(smallDict, tables);

            //Assert
            //TODO could be 1 instead of 2, AUTOBOTS REGROUP
            Assert.AreEqual(2,tablesSmall.Count);
        }

        [TestMethod]
        public void GetLargestTableNoSeats()
        {
            //Setup
            var tblCtrl = new TableCtrl();
            #region tablesList

            var id = 1000000;
            List<Table> tables = new List<Table>();
            tables.Add(new Table { NoSeats = 4, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 9, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 2, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 5, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 5, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 7, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 8, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 9, Reserved = false, RestaurantId = id });
            tables.Add(new Table { NoSeats = 10, Reserved = false, RestaurantId = id });

            #endregion

            #region smallDict

            var smallDict = tblCtrl.FindSmallestModulo(tblCtrl.GetAllModulo(tables, 6));

            #endregion

            //Act
            var tablesSmall = tblCtrl.GetTablesSmallestModulo(smallDict, tables);
        }
    }
}
