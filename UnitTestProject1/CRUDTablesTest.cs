using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ControllerLibrary;
using DatabaseAccessLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;

namespace UnitTests
{
    [TestClass]
    public class CRUDTablesTest
    {
        [TestMethod]
        [DataRow("1000000","4","20","0",true,DisplayName = "All valid")]
        [DataRow("1000000","","","",false,DisplayName = "Empty")]
        [DataRow("1000000","4","20","30",false,DisplayName = "Reserved tables > Total tables")]
        [DataRow("1000000","4","20","-15",false,DisplayName = "Reserved negative")]
        [DataRow("1000000","4","20","1t0",false,DisplayName = "Reserved has letter")]
        [DataRow("1000000","4","20","1£0",false,DisplayName = "Reserved has symbol")]
        [DataRow("1000000","4","20","   0  ",false,DisplayName = "Reserved has space")]
        [DataRow("1000000","4","-20","0",false,DisplayName = "Total tables negative")]
        [DataRow("1000000","4","2t0","0",false,DisplayName = "Total tables has letter")]
        [DataRow("1000000","4","2£0","0",false,DisplayName = "Total tables has symbol")]
        [DataRow("1000000","4","    2 0  ","0",false,DisplayName = "Total has space")]
        [DataRow("1000000","-4","20","0",false,DisplayName = "NoSeats negative")]
        [DataRow("1000000","4t","20","0",false,DisplayName = "NoSeats has letter")]
        [DataRow("1000000","4$","20","0",false,DisplayName = "NoSeats has symbol")]
        [DataRow("1000000"," 4  ","20","0",false,DisplayName = "NoSeats has space")]
        public void Create_Table_Valid_Inputs(string restaurantId, string noSeats, string total, string reserved, bool shouldValidate)
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
                noSeats = 4, reserved = 0, restaurantId = 1000000, total = 20
            };

            var tblDb = new TableDb();

            //Act
            tblDb.AddTable(newTable);

            //Get table with: noSeats = 4, restaurantId = 1000000
            ResTable resTable = tblDb.GetTable(4,1000000);

            //Assert
            //Assert
            Assert.IsTrue(resTable.noSeats == newTable.noSeats
                          && resTable.reserved == newTable.reserved
                          && resTable.total == newTable.total
                          && resTable.restaurantId == newTable.restaurantId);
        }

        [TestMethod]
        public void Create_Table_ModelLayer_To_DbModel()
        {
            //Setup
            var table = new Table
            {
                RestaurantId = "1000000", NoSeats = "4", Reserved = "0", Total = "20"
            };
            var newTable = new ResTable
            {
                noSeats = 4, reserved = 0, restaurantId = 1000000, total = 20
            };
            var tblCtrl = new TableCtrl();

            //Act
            var resTable = tblCtrl.ConvertTable(table);

            //Assert
            Assert.IsTrue(resTable.noSeats == newTable.noSeats
                          && resTable.reserved == newTable.reserved
                          && resTable.total == newTable.total
                          && resTable.restaurantId == newTable.restaurantId);
        }

        [TestMethod]
        public void Delete_No_Tables_Decrement_Db()
        {
            //Setup
            var table = new Table
            {
                RestaurantId = "1000000", NoSeats = "4", Reserved = "0", Total = "20"
            };
            var tblCtrl = new TableCtrl();
            var prevNo = tblCtrl.GetTables().Count();

            //Act
            tblCtrl.DeleteTable(table);
            var currNo = tblCtrl.GetTables().Count();

            //Assert
            Assert.AreEqual(prevNo, currNo+1);
        }

        [TestMethod]
        public void Delete_Correct_Table_Db()
        {
            //Setup
            var table = new Table
            {
                RestaurantId = "1000000", NoSeats = "4", Reserved = "0", Total = "20"
            };
            var tblCtrl = new TableCtrl();

            //Act
            tblCtrl.DeleteTable(table);
            var resTable = tblCtrl.GetTable(table);

            //Assert
            Assert.IsNull(resTable);
        }

        [TestMethod]
        [DataRow(4,0,20)]
        public void Edit_Table(int noSeats, int reserved, int total)
        {

        }
    }
}
