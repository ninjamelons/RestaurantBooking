using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class FindRestaurant
    {
        public void button_Click(object sender, EventArgs e)
        {
            JustFeastDbDataContext con = new JustFeastDbDataContext();
            SqlCommand command = new SqlCommand();
            SqlDataReader dataReader;
            String sql, Output = "";

            sql = "Select name,address,zipcode,phoneNo from dbo.Restaurant";

            command = new SqlCommand(sql);

            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(0) + "-" + dataReader.GetValue(1) + "\n";
            }
            
        }
    }
}
