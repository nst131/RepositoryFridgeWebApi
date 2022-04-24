using FridgeWebApiBL.Models.ProductsBL.Interfaces;
using FridgeWebApiDL.Entity;

namespace FridgeWebApiBL.Models.ProductsBL.Fetchers
{
    public class ProductQueries : IProductQueries
    {
        public string QueryGetProductsByFridgeId(string database, int id)
        {
            return $@"
                        Use {database}
                        Select P.{nameof(Products.Id)},
                               P.{nameof(Products.Name)},
                               FP.{nameof(FridgeProducts.Id)},
                               FP.{nameof(FridgeProducts.Quantity)}
                        From {nameof(FridgeProducts)} as FP
                        JOIN {nameof(Products)} as P
                        ON FP.{nameof(FridgeProducts.ProductId)} = P.Id
                        Where FP.{nameof(FridgeProducts.FridgeId)} = {id}
                    ";
        }

        public string QueryGetFridgeId(int fridgeId, string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select {nameof(Fridge.Id)} From {nameof(Fridge)} Where {nameof(Fridge.Id)} = {fridgeId}
                    ";
        }

        public string QueryGetProductId(int productId, string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select {nameof(Products.Id)} From {nameof(Products)} Where {nameof(Products.Id)} = {productId}
                    ";
        }

        public string QueryGetFridgeProductsId(int fridgeId, int productId, string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select {nameof(FridgeProducts.Id)} From {nameof(FridgeProducts)} 
                        Where {nameof(FridgeProducts.FridgeId)} = {fridgeId} And {nameof(FridgeProducts.ProductId)} = {productId}
                    ";
        }

        public string QueryOnExistFridgeProduct(int fridgeProductId, string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select {nameof(FridgeProducts.Id)} From {nameof(FridgeProducts)} 
                        Where {nameof(FridgeProducts.Id)} = {fridgeProductId}
                    ";
        }

        public string QueryGetFridgeProduct(int fridgeId, int productId, string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select FP.{nameof(FridgeProducts.Id)},
                               FP.{nameof(FridgeProducts.FridgeId)},
                               FP.{nameof(FridgeProducts.ProductId)},
                               FP.{nameof(FridgeProducts.Quantity)}
                        From {nameof(FridgeProducts)} as FP
                        Where {nameof(FridgeProducts.FridgeId)} = {fridgeId} And {nameof(FridgeProducts.ProductId)} = {productId}
                    ";
        }
    }
}
