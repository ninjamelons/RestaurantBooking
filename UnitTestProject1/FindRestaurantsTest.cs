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
using DatabaseAccessLibrary;
using UserWebClient.Controllers;
using UserWebClient.RestaurantService;

namespace UnitTests
{
        
    [TestClass]
    public class FindRestaurantsTest
    {
        [TestMethod]
        [DataRow("910", false, DisplayName = "Short Zip Code")]
        [DataRow("9100000", false, DisplayName = "Too Long Zip Code")]
        [DataRow("9100", true, DisplayName = "")]
        [DataRow(null, false, DisplayName = "Null Zip Code")]
        [DataRow("$", false, DisplayName = "No symbols allowed")]
        [DataRow(" ", false, DisplayName = "No white space allowed")]
        public void Test_Search_Restaurant(int expectedNoOfResults)
        {
            var restaurantServiceStub = new Mock<IRestaurantService>();

            restaurantServiceStub.Setup(x => x.GetAllRestaurants()).Returns(() =>
            {
                return new List<Restaurant> {
                    new Restaurant{zipcode = "9000"}

                }.ToArray();
            });

            var sut = new HomeController(restaurantServiceStub.Object);

            //Act

            ViewResult resultPage = sut.Searches() as ViewResult;

            //Assert

            var model = resultPage.ViewData.Model as IEnumerable<Restaurant>;
            Assert.IsTrue(model.Count() == expectedNoOfResults);
        }

        public void Show_all_restaurants()
        {
            var restaurantServiceStub = new Mock<IRestaurantService>();
            restaurantServiceStub.Setup(x => x.GetAllRestaurants());
        }
    }
    
    
   
}
