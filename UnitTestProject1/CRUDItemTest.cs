using System;
using System.Collections.Generic;
using Moq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;

namespace UnitTestProject1
{
    [TestClass]
    public class CRUDItemTest
    {
      
            [TestMethod]
            [DataRow("ChickenSteak", "WeirdDescription", 12.0, true, DisplayName = "All data correct")]
            [DataRow("a", "WeirderDescription", 12.0, false, DisplayName = "Name too short")]
            [DataRow("asdjkddsfjdsfjkdfsjkdsfkjdfjkdfsjksdfjksdfjksdfjkdsfjkdksfsdkjsdfkjh", "Weird description", 12.0, false, DisplayName = "name too long")]
            [DataRow(null, "Weird Description", 12.0, false, DisplayName = "Null name")]
            [DataRow("GoodName", "012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
                "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789" +
                "01234567890123456789012345678901234567890123456789", 12.0, false, DisplayName = "Description Too long")]
            [DataRow("GoodName", null, 12.0, false, DisplayName = "Null Description")]
            [DataRow("GoodName", "GoodDescription", -12.0, false, DisplayName = "Price < 0")]
            [DataRow("GoodName", "GoodDescription", null, false, DisplayName = "Null Price")]
            public void Create_Table_Valid_Inputs(string name, string description, double price, bool shouldValidate)
            {
                //Setup
                var sut = new Item
                {
                    Name = name,
                    Description = description,
                    Price = price,
                };

                var context = new ValidationContext(sut, null, null);
                var result = new List<ValidationResult>();

                //Act
                var isModelStateValid = Validator.TryValidateObject(sut, context, result, true);

                //Assert
                Assert.IsTrue(isModelStateValid == shouldValidate);
            }
        }
    }
