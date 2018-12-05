using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    /*[KnownType(typeof(DateTime))]*/
    public class Price
    {
        public double VarPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override string ToString()
        {
            return VarPrice.ToString();
        }
    }
}
