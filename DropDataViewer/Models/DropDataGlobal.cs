namespace DDV.Models
{
    public class DropDataGlobal
    {
        public int Id { get; set; }
        public int Continent { get; set; }
        public int ItemId { get; set; }
        public int Minimum_Quantity { get; set; }
        public int Maximum_Quantity { get; set; }
        public int QuestId { get; set; }
        public int Chance { get; set; }
        public string Comments { get; set; } = string.Empty;
    }
}
