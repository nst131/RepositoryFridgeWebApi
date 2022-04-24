using FridgeWebApiDL.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FridgeWebApiDL.Helper
{
    public static class Helper
    {
        public static async Task ExecuteQuery(this SqlConnection connection, CancellationToken token = default, params string[] queries)
        {
            if (connection.State != ConnectionState.Open)
                throw new ArgumentException();
            var command = new SqlCommand();

            foreach (var query in queries)
            {
                command.CommandText = query;
                command.Connection = connection;
                try
                {
                    await command.ExecuteNonQueryAsync(token);
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Query is not correct working, StackTrace{e.StackTrace}");
                }
            }
        }

        public static async Task<ICollection<Entity>> ExecuteQueryAndRead<Entity>(this SqlConnection connection, string query, CancellationToken token = default)
            where Entity : class, IEntity, new()
        {
            if (connection.State != ConnectionState.Open)
                throw new ArgumentException();

            var adapter = new SqlDataAdapter(query, connection);
            var ds = new DataSet();
            adapter.Fill(ds);

            var table = ds.Tables[0];
            var entity = new Entity();
            var result = new List<Entity>();

            if (token.IsCancellationRequested)
                throw new TaskCanceledException();

            foreach (DataRow row in table.Rows)
            {
                for (var i = 0; i < row.ItemArray.Length; i++)
                {
                    typeof(Entity).GetProperty(table.Columns[i].ColumnName)!?.SetValue(entity, row.ItemArray[i]);
                }

                result.Add(entity.Clone() as Entity);
            }

            await Task.CompletedTask;
            return result;
        }

        public static async Task<ICollection<Entity>> ExecuteQueryAndRead<Entity, JoinEntity>(this SqlConnection connection, string query, CancellationToken token = default)
            where Entity : class, IEntity, new()
            where JoinEntity : class, IEntity, new()
        {
            if (connection.State != ConnectionState.Open)
                throw new ArgumentException();

            var adapter = new SqlDataAdapter(query, connection);
            var ds = new DataSet();
            adapter.Fill(ds);

            var table = ds.Tables[0];
            var entity = new Entity();
            var result = new List<Entity>();

            var joinEntity = new JoinEntity();
            var namesJoinEntity = typeof(JoinEntity).GetProperties();

            if (token.IsCancellationRequested)
                throw new TaskCanceledException();

            foreach (DataRow row in table.Rows)
            {
                for (var i = 0; i < row.ItemArray.Length; i++)
                {
                    var prop = typeof(Entity).GetProperty(table.Columns[i].ColumnName);

                    if (prop is null)
                    {

                        var name = table.Columns[i].ColumnName;

                        if (namesJoinEntity.ToList().Exists(x => x.Name == name))
                        {
                            typeof(JoinEntity).GetProperty(name)!?.SetValue(joinEntity, row.ItemArray[i]);
                        }
                        else
                        {
                            typeof(JoinEntity).GetProperty(name.Remove(name.Length - 1))!?.SetValue(joinEntity, row.ItemArray[i]);
                        }
                    }
                    else
                    {
                        prop.SetValue(entity, row.ItemArray[i]);
                    }
                }

                var proper = typeof(Entity).GetProperty(typeof(JoinEntity).Name);
                proper?.SetValue(entity, joinEntity.Clone());

                result.Add(entity.Clone() as Entity);
            }

            await Task.CompletedTask;
            return result;
        }

        public static async Task<ICollection<Entity>> ExecuteQueryAndRead<Entity, JoinEntity, JoinEntity1>(this SqlConnection connection, string query, CancellationToken token = default)
            where Entity : class, IEntity, new()
            where JoinEntity : class, IEntity, new()
            where JoinEntity1 : class, IEntity, new()

        {
            if (connection.State != ConnectionState.Open)
                throw new ArgumentException();

            var adapter = new SqlDataAdapter(query, connection);
            var ds = new DataSet();
            adapter.Fill(ds);

            var table = ds.Tables[0];
            var entity = new Entity();
            var result = new List<Entity>();

            var namesJoinEntity = typeof(JoinEntity).GetProperties();
            var namesJoinEntity1 = typeof(JoinEntity1).GetProperties();

            if (token.IsCancellationRequested)
                throw new TaskCanceledException();

            foreach (DataRow row in table.Rows)
            {
                var joinEntity = new JoinEntity();
                var joinEntity1 = new JoinEntity1();

                for (var i = 0; i < row.ItemArray.Length; i++)
                {
                    var prop = typeof(Entity).GetProperty(table.Columns[i].ColumnName);

                    if (prop is null)
                    {
                        var name = table.Columns[i].ColumnName;

                        if (namesJoinEntity.ToList().Exists(x => x.Name == name))
                        {
                            typeof(JoinEntity).GetProperty(name)!?.SetValue(joinEntity, row.ItemArray[i]);
                            continue;
                        }

                        if (namesJoinEntity1.ToList().Exists(x => x.Name == name))
                        {
                            typeof(JoinEntity1).GetProperty(name)!?.SetValue(joinEntity1, row.ItemArray[i]);
                            continue;
                        }

                        name = name.Remove(name.Length - 1);

                        if (namesJoinEntity.ToList().Exists(x => x.Name == name))
                        {
                            var propName = typeof(JoinEntity).GetProperty(name);
                            if (propName!?.GetValue(joinEntity) is null)
                            {
                                propName!.SetValue(joinEntity, row.ItemArray[i]);
                                continue;
                            }
                        }

                        if (namesJoinEntity1.ToList().Exists(x => x.Name == name))
                        {
                            var propName = typeof(JoinEntity1).GetProperty(name);
                            if (propName!?.GetValue(joinEntity1) is null)
                            {
                                propName!.SetValue(joinEntity1, row.ItemArray[i]);
                            }
                        }
                    }
                    else
                    {
                        prop.SetValue(entity, row.ItemArray[i]);
                    }
                }

                typeof(Entity).GetProperty(typeof(JoinEntity).Name)?.SetValue(entity, joinEntity);
                typeof(Entity).GetProperty(typeof(JoinEntity1).Name)?.SetValue(entity, joinEntity1);

                result.Add(entity.Clone() as Entity);
            }

            await Task.CompletedTask;
            return result;
        }
    }
}
