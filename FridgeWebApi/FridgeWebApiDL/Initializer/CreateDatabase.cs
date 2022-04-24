using System;
using System.Data;
using System.Threading.Tasks;
using FridgeWebApiDL.Helper;
using FridgeWebApiDL.Interfaces;
using Microsoft.Data.SqlClient;

namespace FridgeWebApiDL.Initializer
{
    internal class CreateDatabase
    {
        public static async Task Initialize(string connectionString)
        {
            await using var myConn = new SqlConnection(connectionString);
            await TryOpenAsync(myConn, new QueryForCreateDatabase());
        }

        private static async Task TryOpenAsync(
            SqlConnection connection,
            IQueryForCreateDatabase queriesForInitializer)
        {
            if (connection is null)
                throw new NullReferenceException();

            if (connection.State is ConnectionState.Closed)
            {
                try
                {
                    await connection.OpenAsync();
                }
                catch
                {
                    var name = connection.Database;
                    var masterCoonectionString = connection.ConnectionString.Replace(name, Database.master);
                    await TryCreateDatabaseWithData(name, masterCoonectionString, queriesForInitializer);
                }
            }
        }

        private static async Task TryCreateDatabaseWithData(
            string nameDatabase,
            string masterConnectionString,
            IQueryForCreateDatabase queriesForInitializer)
        {
            await using var connect = new SqlConnection(masterConnectionString);

            try
            {
                await connect.OpenAsync();
            }
            catch
            {
                throw new ArgumentException("Cann't connect to database");
            }

            await connect.ExecuteQuery( default,
                queriesForInitializer.QueryCreateDatabase(nameDatabase),
                queriesForInitializer.QueryCreateAllTables(nameDatabase),
                queriesForInitializer.QueryFirstInitializer(nameDatabase));
        }
    }
}
