namespace michael_villarrubia_pantry_collab_BE.DTOs
{
    public class PantryItemDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public double Weight { get; set; }
        public int Calories { get; set; }
        public int QuantityInPantry { get; set; }
    }
}
