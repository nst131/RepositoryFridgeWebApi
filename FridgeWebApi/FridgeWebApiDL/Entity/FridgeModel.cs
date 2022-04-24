using System.Collections.Generic;
using FridgeWebApiDL.Interfaces;

namespace FridgeWebApiDL.Entity
{
    public class FridgeModel : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Year { get; set; }
        public ICollection<Fridge> Fridges { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
