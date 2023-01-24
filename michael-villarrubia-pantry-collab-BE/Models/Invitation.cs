using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace michael_villarrubia_pantry_collab_BE.Models
{
    public class Invitation
    {
        public int Id { get; set; }
        public int SenderFamilyId { get; set; }
        public Family SenderFamily { get; set; }
        public int ReceiverFamilyId { get; set; }
        public Family ReceiverFamily { get; set; }
        public bool? Accepted { get; set; } = null;

    }
}
