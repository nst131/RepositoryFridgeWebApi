using FridgeWebApiDL.Entity;

namespace FridgeWebApiDL.Repositories
{
    public class FridgeModelRepository : BaseRepository<FridgeModel>
    {
        public FridgeModelRepository(string connectionString) : base(connectionString) { }

        public override string QueryGetAll(string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select * From {nameof(FridgeModel)}
                    ";
        }

        public override string QueryGet(int id, string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select * From {nameof(FridgeModel)}
                        Where {nameof(FridgeModel.Id)} = {id}
                    ";
        }

        public override string QueryCreate(FridgeModel item, string nameDatabase)
        {
            throw new System.NotImplementedException();
        }

        public override string QueryUpdate(FridgeModel item, string nameDatabase)
        {
            throw new System.NotImplementedException();
        }

        public override string QueryDelete(int id, string nameDatabase)
        {
            throw new System.NotImplementedException();
        }
    }
}
