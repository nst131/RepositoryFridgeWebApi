namespace FridgeWebApiBL.Models.FridgeBL.Dto
{
    public class AcceptUpdateFridgeDtoBL
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int FridgeModelId { get; set; }
        public int UserId { get; set; }
    }
}