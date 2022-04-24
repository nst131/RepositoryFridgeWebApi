namespace FridgeWebApiBL.Models.ProductsBL.Dto.FetchersDto
{
    public class ResponseSearchProductsIntoFridgeDtoBL
    {
        public int Id { get; set; }
        public int FridgeId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int DefaultQuantity { get; set; }
        public string FridgeName { get; set; }
    }
}
