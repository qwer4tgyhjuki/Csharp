using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication2.Models
{
    public class AnimeModel
    {
        [JsonIgnore]
        public int Id {  get; set; }

        [JsonPropertyName("Id")]
        public int GetId => Id;
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; } = "";
        [Required]
        public DateTime? ReleaseYear { get; set; } = DateTime.Now;

    }

}
