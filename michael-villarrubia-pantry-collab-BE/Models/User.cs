namespace michael_villarrubia_pantry_collab_BE.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int? FamilyId { get; set; }
    }
}
