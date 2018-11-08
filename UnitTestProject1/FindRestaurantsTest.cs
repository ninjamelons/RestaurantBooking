using Moq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using UserWebClient.Models;

namespace UnitTestProject1
{
        

     class FindRestaurantsTest
    {
        [TestMethod]
        [DataRow("910", false, DisplayName = "Short Zip Code")]
        [DataRow("9100000", false, DisplayName = "Too Long Zip Code")]
        [DataRow("9100", true, DisplayName = "")]
        [DataRow(null, false, DisplayName = "Null Zip Code")]
        [DataRow("$", false, DisplayName = "No symbols allowed")]
        [DataRow(" ", false, DisplayName = "No white space allowed")]
        public void Test_Search_Restaurant(int zipCode, string searchQuery, int expectedNoOfResults)
        {
            var searchRestaurantStub = new Mock<SearchRestaurant>();

            searchRestaurantStub.Setup(x => x.GetMovie()).Returns(() =>
            {
                return new List<SearchRestaurant> {
                    new SearchRestaurant{zipCode = "9000"}

                }.ToArray();
            });

            var sut = new SearchRestaurant(searchRestaurantStub.Object);

            //Act

            ViewResult resultPage = sut.Index(searchQuery) as ViewResult;

            //Assert

            var model = resultPage.ViewData.Model as IEnumerable<Restaurant>;
            Assert.IsTrue(model.Count() == expectedNoOfResults);
        }
    }
    
    
   
}
