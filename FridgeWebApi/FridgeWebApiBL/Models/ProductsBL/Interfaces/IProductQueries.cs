namespace FridgeWebApiBL.Models.ProductsBL.Interfaces
{
    public interface IProductQueries
    {
        string QueryGetProductsByFridgeId(string database, int id);
        string QueryGetFridgeId(int fridgeId, string nameDatabase);
        string QueryGetProductId(int productId, string nameDatabase);
        string QueryGetFridgeProductsId(int fridgeId, int productId, string nameDatabase);
        string QueryGetFridgeProduct(int fridgeId, int productId, string nameDatabase);
        string QueryOnExistFridgeProduct(int fridgeProductId, string nameDatabase);
    }
}