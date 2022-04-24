namespace FridgeWebApiBL.Models.FridgeBL.Interfaces
{
    public interface IFridgeQueries
    {
        string QueryGetFridgeId(int fridgeId, string nameDatabase);
        string QueryGetFridgeModelId(int fridgeModelId, string nameDatabase);
        string QueryGetUserId(int userId, string nameDatabase);
    }
}