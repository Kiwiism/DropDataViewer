using System.ComponentModel.DataAnnotations.Schema;

namespace DDV.Models
{
    public class DropData
    {
        public int Id { get; set; }
        public int DropperId { get; set; }
        public int ItemId { get; set; }
        public int Minimum_Quantity { get; set; }
        public int Maximum_Quantity { get; set; }
        public int QuestId { get; set; }
        public int Chance { get; set; }
    }
}
