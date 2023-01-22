namespace michael_villarrubia_pantry_collab_BE.Models
{
    public class RecipeAccess
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int FamilyId { get; set; }
        public Family Family { get; set; }
    }
}
