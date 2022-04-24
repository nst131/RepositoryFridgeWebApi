using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;

namespace FridgeWebApiDL.Repositories
{
    public class FridgeProductsRepository : BaseRepository<FridgeProducts>
    {
        public FridgeProductsRepository(string connectionString) : base(connectionString) { }

        public override string QueryGetAll(string nameDatabase)
        {
            return $@"
                                Use {nameDatabase}
                                Select FP.{nameof(FridgeProducts.Id)},
                                       FP.{nameof(FridgeProducts.FridgeId)},
                                       FP.{nameof(FridgeProducts.ProductId)},
                                       FP.{nameof(FridgeProducts.Quantity)}  
                                From {nameof(FridgeProducts)} as FP
                            ";
        }

        public override string QueryGet(int id, string nameDatabase)
        {
            return $@"
                                Use {nameDatabase}
                                Select FP.{nameof(FridgeProducts.Id)},
                                       FP.{nameof(FridgeProducts.FridgeId)},
                                       FP.{nameof(FridgeProducts.ProductId)},
                                       FP.{nameof(FridgeProducts.Quantity)}  
                                From {nameof(FridgeProducts)} as FP
                                Where FP.{nameof(FridgeProducts.Id)} = {id}
                            ";
        }

        public override string QueryCreate(FridgeProducts item, string nameDatabase) //Create Products Into Fridge
        {
            return $@"
                                Use {nameDatabase}
                                {DML.Insert} Into {nameof(FridgeProducts)}
                                ({nameof(FridgeProducts.Quantity)}, {nameof(FridgeProducts.FridgeId)}, {nameof(FridgeProducts.ProductId)})
                                Values
                                ({item.Quantity}, {item.FridgeId}, {item.ProductId})
                            ";
        }

        public override string QueryUpdate(FridgeProducts item, string nameDatabase) //Update Products Into Fridge
        {
            return $@"
                                Use {nameDatabase}
                                {DDL.Update} {nameof(FridgeProducts)}
                                Set 
                                    {nameof(FridgeProducts.Quantity)} = {item.Quantity}
                                Where {nameof(FridgeProducts.Id)} = {item.Id}
                            ";
        }

        public override string QueryDelete(int id, string nameDatabase) //Delete Products Into Fridge
        {
            return $@"
                                Use {nameDatabase}
                                Delete From {nameof(FridgeProducts)}
                                Where {nameof(FridgeProducts.Id)} = {id}
                            ";
        }
    }
}
