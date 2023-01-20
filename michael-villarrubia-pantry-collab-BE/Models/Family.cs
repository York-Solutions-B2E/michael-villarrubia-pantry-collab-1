﻿using System.Text.Json.Serialization;

namespace michael_villarrubia_pantry_collab_BE.Models
{
    public class Family
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<User> Users { get; set; }
        [JsonIgnore]
        public Pantry Pantry { get; set; } = new Pantry();
        [JsonIgnore]
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
