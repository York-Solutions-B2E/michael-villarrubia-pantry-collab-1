﻿namespace michael_villarrubia_pantry_collab_BE.Models
{
    public class Family
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public List<User> Users { get; set; }
    }
}
