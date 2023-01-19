using System.Text.Json.Serialization;

namespace michael_villarrubia_pantry_collab_BE.Models
{
    public class Pantry
    {
        public int Id { get; set; }
        public List<PantryItem> Items { get; set; } = new List<PantryItem>();
        public int FamilyId { get; set; }
        [JsonIgnore]
        public Family Family { get; set; }
    }
}
