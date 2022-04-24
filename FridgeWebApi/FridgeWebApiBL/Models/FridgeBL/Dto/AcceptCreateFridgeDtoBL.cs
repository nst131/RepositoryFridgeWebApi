namespace FridgeWebApiBL.Models.FridgeBL.Dto
{
    public class AcceptCreateFridgeDtoBL
    {
        public string Name { get; set; } = string.Empty;
        public int FridgeModelId { get; set; }
        public int UserId { get; set; }
    }
}