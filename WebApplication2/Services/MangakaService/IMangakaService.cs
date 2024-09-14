using WebApplication2.Models;

namespace WebApplication2.Services.MangakaService
{
    public interface IMangakaService
    {
        public Task<MangakaModel> GetMangakaById(int id);
        public Task<IEnumerable<MangakaModel>> GetAllMangakas();
        public Task<List<MangakaModel>> DeleteMangakaById(int id);
        public Task<List<MangakaModel>> DeleteAnimeFromMangaka(int mangakaId, int animeId);

        public Task<MangakaModel> AddAnimeToMangaka(int id, string anime);
        public Task<MangakaModel> UpdateMangaka(int id, MangakaModel mangakaModel);
    }
}
