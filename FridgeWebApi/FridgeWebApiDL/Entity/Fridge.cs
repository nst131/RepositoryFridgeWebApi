using FridgeWebApiDL.Interfaces;
using System.Collections.Generic;

#nullable enable
namespace FridgeWebApiDL.Entity
{
    public class Fridge : IEntity, IEntityName
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int FridgeModelId { get; set; }
        public FridgeModel FridgeModel { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<FridgeProducts> FridgeProducts { get; set; } = null!;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
