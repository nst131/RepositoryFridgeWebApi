using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;
using Microsoft.Data.SqlClient;

namespace FridgeWebApiDL.Repositories
{
    public class FridgeRepository : BaseRepository<Fridge>
    {
        private readonly string connectionString;
        public FridgeRepository(string connectionString) : base(connectionString)
        {
            this.connectionString = connectionString;
        }

        public override async Task<ICollection<Fridge>> GetAll(CancellationToken token = default)
        {
            await using var connect = new SqlConnection(connectionString);
            await connect.OpenAsync(token);
            return await connect.ExecuteQueryAndRead<Fridge, FridgeModel, User>(this.QueryGetAll(connect.Database), token);
        }

        public override string QueryGetAll(string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select F.{nameof(Fridge.Id)},
                               F.{nameof(Fridge.Name)},
                               U.{nameof(User.UserName)},
                               FM.{nameof(FridgeModel.Name)}
                        From {nameof(Fridge)} as F
                        JOIN {nameof(FridgeModel)} as FM
                            ON F.{nameof(Fridge.FridgeModelId)} = FM.{nameof(FridgeModel.Id)}
                        JOIN [{nameof(User)}] as U
                            ON F.{nameof(Fridge.UserId)} = U.{nameof(User.Id)}
                    ";
        }

        public override async Task<Fridge> Get(int id, CancellationToken token = default)
        {
            await using var connect = new SqlConnection(connectionString);
            await connect.OpenAsync(token);
            var element = await connect.ExecuteQueryAndRead<Fridge, FridgeModel, User>(this.QueryGet(id, connect.Database), token);
            return element.FirstOrDefault();
        }

        public override string QueryGet(int id, string nameDatabase)
        {
            return $@"
                                Use {nameDatabase}
                                Select F.{nameof(Fridge.Id)},
                                       F.{nameof(Fridge.Name)},
                                       U.{nameof(User.UserName)},
                                       FM.{nameof(FridgeModel.Name)}
                                From {nameof(Fridge)} as F
                                JOIN {nameof(FridgeModel)} as FM
                                    ON F.{nameof(Fridge.FridgeModelId)} = FM.{nameof(FridgeModel.Id)}
                                JOIN [{nameof(User)}] as U
                                    ON F.{nameof(Fridge.UserId)} = U.{nameof(User.Id)}
                                Where F.{nameof(Fridge.Id)} = {id}
                            ";
        }

        public override string QueryCreate(Fridge item, string nameDatabase)
        {
            return $@"
                                Use {nameDatabase}
                                {DML.Insert} Into {nameof(Fridge)}
                                ({nameof(Fridge.Name)}, {nameof(Fridge.FridgeModelId)}, {nameof(Fridge.UserId)})
                                Values
                                ('{item.Name}', {item.FridgeModelId}, {item.UserId})
                            ";
        }

        public override string QueryUpdate(Fridge item, string nameDatabase)
        {
            return $@"
                                Use {nameDatabase}
                                {DDL.Update} {nameof(Fridge)}
                                Set 
                                    {nameof(Fridge.Name)} = '{item.Name}',
                                    {nameof(Fridge.FridgeModelId)} = {item.FridgeModelId},
                                    {nameof(Fridge.UserId)} = {item.UserId}
                                Where {nameof(Fridge.Id)} = {item.Id}
                            ";
        }

        public override string QueryDelete(int id, string nameDatabase)
        {
            return $@"
                                Use {nameDatabase}
                                Delete From {nameof(Fridge)}
                                Where {nameof(Fridge.Id)} = {id}
                            ";
        }
    }
}
