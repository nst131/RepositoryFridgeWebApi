using System;
using FridgeWebApiDL.Context;
using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;
using System.Threading.Tasks;
using FridgeWebApiBL.Models.ProductsBL.Dto.FetchersDto;
using FridgeWebApiBL.Models.ProductsBL.Interfaces;
using Microsoft.Data.SqlClient;

namespace FridgeWebApiBL.Models.ProductsBL.Fetchers
{
    public class ProductProcedures : IProductProcedures
    {
        private readonly IDbContext context;

        public ProductProcedures(IDbContext context)
        {
            this.context = context;
        }

        public async Task CreateProcedureSearchProductsIntoFridge()
        {
            await this.context.ExecuteQuery(
                default,
                this.UseDatabase(this.context.GetDatabase),
                this.QueryOnCreateProcedureSearchProductsIntoFridge());
        }

        private string UseDatabase(string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                    ";
        }

        private string QueryOnCreateProcedureSearchProductsIntoFridge()
        {
            return $@"
                        {DDL.Create} Procedure {NameProductsProcedures.ProcedureSearchProductsInFridge}
                        As
                            Select FP.{nameof(FridgeProducts.Id)},
                                   FP.{nameof(FridgeProducts.FridgeId)},
                                   FP.{nameof(FridgeProducts.ProductId)},
                                   P.{nameof(Products.Name)},
                                   P.{nameof(Products.DefaultQuantity)},
                                   F.{nameof(Fridge.Name)}
                            From {nameof(FridgeProducts)} as FP
                            Join {nameof(Products)} as P
                            On FP.{nameof(FridgeProducts.ProductId)} = P.{nameof(Products.Id)}
                            Join {nameof(Fridge)} as F
                            On FP.{nameof(FridgeProducts.FridgeId)} = F.{nameof(Fridge.Id)}
                            Where FP.{nameof(FridgeProducts.Quantity)} = 0
                    ";
        }

        public ResponseSearchProductsIntoFridgeDtoBL GetModelFromProcedureSearchProductsIntoFridge(SqlDataReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));

            return new ResponseSearchProductsIntoFridgeDtoBL()
            {
                Id = reader.GetInt32(0),
                FridgeId = reader.GetInt32(1),
                ProductId = reader.GetInt32(2),
                ProductName = reader.GetString(3),
                DefaultQuantity = reader.GetInt32(4),
                FridgeName = reader.GetString(5)
            };
        }
    }
}
