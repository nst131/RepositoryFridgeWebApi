using FridgeWebApiDL.Interfaces;

namespace FridgeWebApiDL.Entity
{
    public class FridgeProducts : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Products Products { get; set; }
        public int FridgeId { get; set; }
        public Fridge Fridge { get; set; }
        public int Quantity { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
