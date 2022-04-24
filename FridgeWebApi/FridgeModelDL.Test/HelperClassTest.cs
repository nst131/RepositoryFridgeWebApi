using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FridgeModelDL.Test
{
    [TestClass]
    public class HelperClassTest
    {
        private const string connectionString = "Server =(localdb)\\MSSQLLocalDB; Database = FridgeDatabase; Trusted_Connection=True;";
        private SqlConnection connection;

        [TestInitialize]
        public void Initiliaze()
        {
            this.connection = new SqlConnection(connectionString);
            connection.Open();
        }

        [TestMethod]
        public void Connection_CheckConnectionWithDatabase_StateOpen()
        {
            Assert.AreEqual(ConnectionState.Open, connection.State, "connectionString not correct or database didn't create");
        }

        [TestMethod]
        public void ExecuteQueryAndRead_ExecuteCorrectlyQuery_FilledCollection()
        {
            string query = @$"
                                Use {connection.Database}
                                Select {nameof(Entity.Id)} From {nameof(Products)} 
                            ";

            var collection = this.connection.ExecuteQueryAndRead<Entity>(query, default).Result;

            CollectionAssert.AllItemsAreNotNull((ICollection)collection);
        }

        [TestMethod]
        public void ExecuteQueryAndRead_CompareTheUniqueProductsNames_Unique()
        {
            string query = $@"
                                Use {connection.Database}
                                Select {nameof(Products.Name)} From {nameof(Products)}
                            ";

            var collectionProducts = this.connection.ExecuteQueryAndRead<Products>(query, default).Result;

            var list = new List<string>();
            collectionProducts.ToList().ForEach(x => list.Add(x.Name));

            CollectionAssert.AllItemsAreUnique(list, $"{nameof(Products)} cannot have the same names");
        }

        [TestMethod]
        public void ExecuteQueryAndRead_CompareTheUniqueFridgesNames_Unique()
        {
            string query = $@"
                                Use {connection.Database}
                                Select {nameof(Fridge.Name)} From {nameof(Fridge)}
                            ";

            var collectionProducts = this.connection.ExecuteQueryAndRead<Fridge>(query, default).Result;

            var list = new List<string>();
            collectionProducts.ToList().ForEach(x => list.Add(x.Name));

            CollectionAssert.AllItemsAreUnique(list, $"{nameof(Fridge)} cannot have the same names");
        }
    }
}
