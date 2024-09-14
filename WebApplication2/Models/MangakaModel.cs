using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;

namespace WebApplication2.Models
{
    public class MangakaModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonPropertyName("Id")]
        public int GetId => Id;
        public string Name { get; set; }
        public int Age { get; set; }
        //[JsonIgnore]
        // public List<AnimeModel> Titles { get; set; } = new List<AnimeModel>();
        //[JsonPropertyName("Titles")]
        //public List<AnimeModel> GetTitles => Titles;

        [JsonIgnore]
        public Dictionary<int, string> Titles { get; set; } = new Dictionary<int, string>();
        [JsonPropertyName("Titles")]
        public Dictionary<int, string> GetTitles => Titles;
    }
}
