using System;
using System.Collections.Generic;
using Moq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class CRUDTables
    {
        [TestMethod]
        public void Create_TableId_Is_Int(int noSeats, int total, bool reserved)
        {
            //Setup
            var sut = new TableModel
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
            Assert.IsTrue();
        }

        [TestMethod]
        public void Create_NoSeats_Is_Int()
        {
        }
        
        [TestMethod]
        public void Create_Reserved_Is_Boolean()
        {
        }
        
        [TestMethod]
        public void Create_Variables_No_Symbol()
        {
        }
        
        [TestMethod]
        public void Create_Variables_Not_Null()
        {
        }
        
        [TestMethod]
        public void Create_Tables_Added_To_Database()
        {
        }
    }
}
