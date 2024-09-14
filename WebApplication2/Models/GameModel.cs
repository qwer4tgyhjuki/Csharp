namespace WebApplication2.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime ReleaseYear { get; set; }

        public int DeveloperCompanyId { get; set;}
        public int PublihserId { get; set;}
        public int RatingId { get; set;}

    }
}
