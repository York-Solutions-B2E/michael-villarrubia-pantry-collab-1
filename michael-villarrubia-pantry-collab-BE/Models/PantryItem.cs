using System.Text.Json.Serialization;

namespace michael_villarrubia_pantry_collab_BE.Models
{
    public class PantryItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public double Weight { get; set; }
        public int Calories { get; set; }
        public int QuantityInPantry { get; set; }
        public int PantryId { get; set; }
        [JsonIgnore]
        public Pantry Pantry { get; set; } = new Pantry();
    }
}
