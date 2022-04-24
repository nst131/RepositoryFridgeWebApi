namespace FridgeWebApiBL.Models.UserBL.Interfaces
{
    public interface IUserQueries
    {
        string QueryCheckUniqueEmail(string email, string nameDatabase);
        string QueryCheckUniqueName(string name, string nameDatabase);
        string QueryGetUserId(int userId, string nameDatabase);
    }
}