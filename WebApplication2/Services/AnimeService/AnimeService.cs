using WebApplication2.Models;

namespace WebApplication2.Services.AnimeService
{
    public class AnimeService : IAnimeService
    {
        private static int id = 1;
        private static List<AnimeModel> anime;
        public AnimeService()
        {
            anime = new List<AnimeModel>
            {
                new AnimeModel {
                    Id = id++,
                    Name = "Naruto",
                    Description = "Naruto Uzumaki, a mischievous adolescent ninja, " +
                    "struggles as he searches for recognition and dreams of becoming " +
                    "the Hokage, the village's leader and strongest ninja.",
                    ReleaseYear = new DateTime(2002, 10, 3)
                },
                new AnimeModel {
                    Id = id++,
                    Name = "Bleach",
                    Description = "Bleach follows the story of Ichigo Kurosaki. " +
                    "When Ichigo meets Rukia he finds his life is changed forever",
                    ReleaseYear = new DateTime(2004, 10, 5)
                },
                new AnimeModel {
                    Id = id++,
                    Name = "Death Note",
                    Description = "An intelligent high school student " +
                    "goes on a secret crusade to eliminate criminals " +
                    "from the world after discovering" +
                    " a notebook capable of killing anyone whose name is written into it.",
                    ReleaseYear = new DateTime(2006, 10, 3)
                },
                new AnimeModel
                {
                    Id = id++,
                    Name = "Vinland Saga",
                    Description = "",
                    ReleaseYear = new DateTime(2019, 8, 15)
                },
                new AnimeModel
                {
                    Id = id++,
                    Name = "Experiments Lain",
                    Description = "",
                    ReleaseYear = new DateTime(1998, 7, 6)
                },
                new AnimeModel
                {
                    Id = id++,
                    Name = "Fullmetal Alchemist: Brotherhood",
                    Description = "",
                    ReleaseYear = new DateTime(2009, 4, 5)
                },
                new AnimeModel
                {
                    Id = id++,
                    Name = "Hunter x Hunter (2011)",
                    ReleaseYear = new DateTime(2011, 10, 2),
                    Description = ""
                }
            };
        }
        public Task<AnimeModel> GetAnimeById(int id)
        {
            var animeFromId = anime.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(animeFromId);
        }
        public Task<IEnumerable<AnimeModel>> GetAllAnime()
        {
            return Task.FromResult<IEnumerable<AnimeModel>>(anime);
        }
        public Task<AnimeModel> AddAnime(AnimeModel animeModel)
        {
            animeModel.Id = id++;
            // animeModel.Id = Guid.NewGuid().ToString();
            anime.Add(animeModel);
            return Task.FromResult(animeModel);
        }

        public Task<List<AnimeModel>> UpdateAnime(int id, AnimeModel updatedModel)
        {
            var oldAnime = anime.FirstOrDefault(x => x.Id == id);
            if (oldAnime == null)
            {
                throw new Exception("Anime not Found");
            }
            oldAnime.Name = updatedModel.Name;
            oldAnime.Description = updatedModel.Description;
            oldAnime.ReleaseYear = updatedModel.ReleaseYear;
            return Task.FromResult(anime);
        }
        public Task<List<AnimeModel>> DeleteAnimeById(int id)
        {
            anime.RemoveAll(a => a.Id == id);
            return Task.FromResult(anime);
        }



        public Task<AnimeModel> PostRandomAnime(AnimeModel animeModel)
        {
            animeModel.Id = id++;
            anime.Add(animeModel);

            return Task.FromResult(animeModel);
        }

        public Task<AnimeModel> GetRandomAnimeToUser()
        {
            Random random = new Random();
            int randomIndex = random.Next(0, anime.Count);

            return Task.FromResult(anime[randomIndex]);
        }
    }
}
