namespace FridgeWebApiDL.Interfaces
{
    public interface IQueryForCreateDatabase
    {
        string QueryCreateAllTables(string name);
        string QueryCreateDatabase(string name); 
        string QueryFirstInitializer(string name);
    }
}
