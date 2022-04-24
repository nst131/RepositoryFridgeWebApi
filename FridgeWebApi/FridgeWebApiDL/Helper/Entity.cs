using System;
using FridgeWebApiDL.Interfaces;

namespace FridgeWebApiDL.Helper
{
    public class Entity : IEntity
    {
        public int Id { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
