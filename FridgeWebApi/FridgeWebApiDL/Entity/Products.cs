using System.Collections.Generic;
using FridgeWebApiDL.Interfaces;

namespace FridgeWebApiDL.Entity
{
    public class Products : IEntity, IEntityName
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? DefaultQuantity { get; set; }
        public int? Quantity { get; set; }
        private ICollection<FridgeProducts> FridgeProducts { get; set; } = null;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
