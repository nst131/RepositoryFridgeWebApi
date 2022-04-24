using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;

namespace FridgeWebApiBL.Models.ProductsBL.Dto.FetchersDto
{
    public class ResponseProductIntoFridgeDtoBL : Entity
    {
        public string Name { get; set; } = string.Empty;
        public FridgeProducts FridgeProducts { get; set; } = null;
    }
}
