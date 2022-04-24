namespace FridgeWebApiBL.Models.ProductsBL.Dto.FetchersDto
{
    public class ResponseGetProductIntoFridgeByIdDtoBL
    {
        public int FridgeProductId { get; set; }
        public int FridgeId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
