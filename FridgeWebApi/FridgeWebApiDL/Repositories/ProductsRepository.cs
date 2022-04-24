using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;

namespace FridgeWebApiDL.Repositories
{
    public class ProductsRepository : BaseRepository<Products>
    {
        public ProductsRepository(string connectionString) : base(connectionString) { }

        public override string QueryGetAll(string nameDatabase)
        {
            return $@"
                                Use {nameDatabase}
                                Select P.{nameof(Products.Id)},
                                       P.{nameof(Products.Name)},
                                       P.{nameof(Products.DefaultQuantity)}
                                From {nameof(Products)} as P
                            ";
        }

        public override string QueryGet(int id, string nameDatabase)
        {
            return $@"
                                Use {nameDatabase}
                                Select P.{nameof(Products.Id)},
                                       P.{nameof(Products.Name)},
                                       P.{nameof(Products.DefaultQuantity)}
                                From {nameof(Products)} as P
                                Where P.{nameof(Products.Id)} = {id}
                            ";
        }

        public override string QueryCreate(Products item, string nameDatabase)
        {
            return $@"
                                Use {nameDatabase}
                                {DML.Insert} Into {nameof(Products)}
                                ({nameof(Products.Name)}, {nameof(Products.DefaultQuantity)})
                                Values
                                ('{item.Name}', {item.DefaultQuantity})
                            ";
        }

        public override string QueryUpdate(Products item, string nameDatabase)
        {
            return $@"
                                Use {nameDatabase}
                                {DDL.Update} {nameof(Products)}
                                Set 
                                    {nameof(Products.Name)} = '{item.Name}',
                                    {nameof(Products.DefaultQuantity)} = {item.DefaultQuantity}
                                Where {nameof(Products.Id)} = {item.Id}
                            ";
        }

        public override string QueryDelete(int id, string nameDatabase)
        {
            return $@"
                                Use {nameDatabase}
                                Delete From {nameof(Products)}
                                Where {nameof(Products.Id)} = {id}
                            ";
        }
    }
}
