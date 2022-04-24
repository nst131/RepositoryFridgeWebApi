namespace FridgeWebApiDL.Interfaces
{
    public interface IEntity
    {
        public int Id { get; set; }
        public object Clone();
    }
}
