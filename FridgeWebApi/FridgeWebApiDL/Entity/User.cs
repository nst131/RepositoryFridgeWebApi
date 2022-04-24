using FridgeWebApiDL.Interfaces;

namespace FridgeWebApiDL.Entity
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
