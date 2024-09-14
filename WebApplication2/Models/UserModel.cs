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


        [Required, EmailAddress]
        public string? Email { get; set; }


        [Required]
        public string? Password { get; set; }

        public DateTime Birth { get; set; }

        [StringLength(15, ErrorMessage = "Maximum Length is 15")]
        public string? FirstName { get; set; }
        [StringLength(15, ErrorMessage = "Maximum Length is 15")]
        public string? LastName { get; set; }

        [JsonIgnore]
        public DateTime LastLogin { get; set; }

        [JsonIgnore]
        public int AttemptsLogin { get; set; }

        [JsonPropertyName("LastLogin")]
        public DateTime GetLastLogin => LastLogin;

        [JsonPropertyName("AttemptsLogin")]
        public int GetAttempsLogin => AttemptsLogin;


        [JsonIgnore]

        public List<AnimeModel> FavouriteAnime { get; set; } = new List<AnimeModel>();

        [JsonPropertyName("Anime")]
        public List<AnimeModel> GetFavouriteAnime => FavouriteAnime;
    }
}
