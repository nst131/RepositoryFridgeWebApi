#nullable enable
namespace FridgeWebApiBL.Models.FridgeBL.Dto
{
    public class ResponseFridgeDtoBL
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? OwnerName { get; set; }
        public string ModelName { get; set; } = string.Empty;
    }
}
