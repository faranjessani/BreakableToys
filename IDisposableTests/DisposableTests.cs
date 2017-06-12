using System.Data.SqlClient;
using NUnit.Framework;

namespace IDisposableTests
{
    [TestFixture]
    public class DisposableTests
    {
        public void NotDisposing()
        {
            using (var sqlConnection = new SqlConnection())
            using (var sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}