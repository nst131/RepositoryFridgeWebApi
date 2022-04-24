namespace FridgeWebApiBL.Models.ProductsBL.Dto.CrudDto
{
    public class ResponseProductDtoBL
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DefaultQuantity { get; set; } = 0;
    }
}
