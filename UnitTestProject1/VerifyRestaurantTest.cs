using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    class VerifyRestaurantTest
    {
        [TestMethod]
        [DataRow("RestaurantName", "Good Address", "+45 50 38 97 04", "ohshit@gmail.com", true, DisplayName =
            "Restaurant is verify!")]
        [DataRow("Restadfds", "Good Address", "+45 50 38 97 04", "ohshit@gmail.com", false, DisplayName =
            "Restaurant is not verify!")]
        public void Test_Restaurant_Verify(string name, string address, string phoneNo, string email,
            bool shouldValidate, bool verify)
        {

        }
    }
}
