using WebApplication2.Models;

namespace WebApplication2.Services.AnimeService
{
    public interface IAnimeService
    {
        public Task<AnimeModel> GetAnimeById(int id);
        public Task<IEnumerable<AnimeModel>> GetAllAnime();
        public Task<AnimeModel> AddAnime(AnimeModel animeModel);

        public Task<List<AnimeModel>> UpdateAnime(int id, AnimeModel animeModel);
        public Task<List<AnimeModel>> DeleteAnimeById(int id);
        public Task<AnimeModel> GetRandomAnimeToUser();
        public Task<AnimeModel> PostRandomAnime(AnimeModel animeModel);
    }
}
