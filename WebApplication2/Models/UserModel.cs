using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication2.Models
{
    public class UserModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("Id")]
        public int GetId => Id;

        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [JsonIgnore]

        public List<AnimeModel> FavouriteAnime { get; set; } = new List<AnimeModel>();

        [JsonPropertyName("Anime")]
        public List<AnimeModel> GetFavouriteAnime => FavouriteAnime;
    }
}
