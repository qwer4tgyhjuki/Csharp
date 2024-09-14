using WebApplication2.Models;

namespace WebApplication2.Services.MangakaService
{
    public class MangakaService : IMangakaService
    {
        private static int id = 1;
        private static List<MangakaModel> mangakas = new List<MangakaModel>
        {
            new MangakaModel
            {
                Id = id++,
                Name = "ONE",
                Age = 38,
                Titles = new Dictionary<int, string>
                {
                    { id++, "One Punch Man" },
                    { id++, "One Punch Man 2" },
                    { id++, "Mob Pyscho" }
                }
            },
            new MangakaModel
            {
                Id = id++,
                Name = "Makoto Yukimura",
                Age = 48,
                Titles = new Dictionary<int, string>
                {
                    { id++, "Vindland Saga" },
                    { id++, "Vinland Saga 2" }
                }
            },
            new MangakaModel
            {
                Id = id++,
                Name = "Kentarou Miura",
                Age = 55,
                Titles = new Dictionary<int, string>
                {
                    {id++, "Bereserk" },
                }
            },
            new MangakaModel
            {
                Id = id++,
                Name = "Hiromu Arakawa",
                Age = 48,
                Titles = new Dictionary<int, string>
                {
                    {id++, "Fullmetal Alchemist" }
                }
            },
            new MangakaModel
            {
                Id = id++,
                Name = "Yoshihiro Togashi",
                Age = 48,
                Titles = new Dictionary<int, string>
                {
                    {id++, "Hunter X Hunter" }
                }
            },
            new MangakaModel
            {
                Id = id++,
                Name = "Hajime Isayama",
                Age = 48,
                Titles = new Dictionary<int, string>
                {
                    {id++, "Shingeki No Kyojin" }
                }
            },

        };
        public Task<List<MangakaModel>> DeleteAnimeFromMangaka(int mangakaId, int animeId)
        {
            var mangaka = mangakas.FirstOrDefault(x => x.Id == mangakaId);
            var anime = mangaka.Titles.Remove(animeId);

            return Task.FromResult(mangakas);
        }

        public Task<List<MangakaModel>> DeleteMangakaById(int id)
        {
            mangakas.RemoveAll(x => x.Id == id);
            return Task.FromResult(mangakas);
        }

        public Task<IEnumerable<MangakaModel>> GetAllMangakas()
        {
            return Task.FromResult<IEnumerable<MangakaModel>>(mangakas);
        }

        public Task<MangakaModel> GetMangakaById(int id)
        {
            var mangaka = mangakas.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(mangaka);
        }
        public Task<MangakaModel> AddAnimeToMangaka(int id, string anime)
        {
            var mangaka = mangakas.FirstOrDefault(x => x.Id == id);
            mangaka.Titles.Add(id++, anime);

            return Task.FromResult(mangaka);
        }

        public Task<MangakaModel> UpdateMangaka(int id, MangakaModel mangakaModel)
        {
            var mangaka = mangakas.FirstOrDefault(x =>x.Id == id);
            mangaka.Name = mangakaModel.Name;
            mangaka.Age = mangakaModel.Age;

            return Task.FromResult(mangaka);
        }

    }
}
