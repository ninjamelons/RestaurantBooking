using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public partial class ResetDb
    {
        protected void clean()
        {
            var db = new JustFeastDbDataContext();

            string dropCreateScript =
                File.ReadAllText(@"C:\Users\Golvin\Documents\UCN\3rd semester\Project\Drop and Create all tables.sql");
            var conn = db.Connection;

            var cmd = new SqlCommand(dropCreateScript);
            conn.BeginTransaction();
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
