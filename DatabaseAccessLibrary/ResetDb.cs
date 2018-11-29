using System.IO;
using System.Net;

namespace DatabaseAccessLibrary
{
    public partial class ResetDb
    {
        private readonly WebClient _client = new WebClient();
        private Stream _streamInsert;

        public string GetCreateFile()
        {
            var streamCreate = _client.OpenRead("https://www.dropbox.com/s/d4aibod4sffqtrx/Drop%20and%20Create%20all%20tables.sql?dl=1");
            var streamReaderCreate = new StreamReader(streamCreate);
            return streamReaderCreate.ReadToEnd();

        }

        public string GetInsertFile()
        {
            var streamInsert = _client.OpenRead("https://www.dropbox.com/s/7emfnd5vhnvdexn/TestData.sql?dl=1");
            var streamReaderInsert = new StreamReader(streamInsert);
            return streamReaderInsert.ReadToEnd();
        }
        public void Clean()
        {
            var db = new JustFeastDbDataContext();
            var conn = db.Connection;
            var cmdCreate = conn.CreateCommand();
            var cmdInsert = conn.CreateCommand();
            cmdCreate.CommandText = GetCreateFile();
            conn.Open();
            cmdInsert.CommandText = GetInsertFile();
            cmdCreate.ExecuteNonQuery();
            cmdInsert.ExecuteNonQuery();
            conn.Close();
        }
    }
}
