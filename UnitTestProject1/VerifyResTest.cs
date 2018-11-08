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

namespace UnitTestProject1
{
    class VerifyResTest
    {
        [TestMethod]
        [DataRow("RestaurantName", "Good Address", "+45 50 38 97 04", "ohshit@gmail.com", true, DisplayName = "Restaurant is verify!")]
        [DataRow("Restadfds", "Good Address", "+45 50 38 97 04", "ohshit@gmail.com", false, DisplayName = "Restaurant is not verify!")]
        public void Test_Restaurant_Verify(string name, string address, string phoneNo, string email, bool shouldValidate, bool verify)

    }
}
