using WebApplication2.Models;
using WebApplication2.Services.AnimeService;

namespace WebApplication2.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IAnimeService _animeService;
        private static int id = 1;
        private static List<UserModel> users = new List<UserModel> {
            new UserModel {
                Id = id++,
                Username = "stalker",
                Password = "123456",
                FavouriteAnime = new List<AnimeModel>()
            },
            new UserModel {
                Id = id++,
                Username = "hashedMan",
                Password = "no_hash-password??",
                FavouriteAnime = new List<AnimeModel>()
            },
            new UserModel {
                Id = id++,
                Username = "Kratos",
                Password = "destr01_a11_g0dZ",
                FavouriteAnime = new List<AnimeModel>()
                },
            new UserModel {
                Id = id++,
                Username = "DOOM Slayer",
                Password = "hrtjiokb15652hvgqwh1487$ufi31",
                FavouriteAnime = new List<AnimeModel>()
                },
            new UserModel {
                Id = id++,
                Username = "Postal_man",
                Password = "daawe1efgwergstrhnyuae501_fweqa1qwet31_g0123dZ",
                FavouriteAnime = new List<AnimeModel>()
                },
            new UserModel {
                Id = id++,
                Username = "Batman",
                Password = "liodes2451245r046i56t1_a11_g0gvwesdxwZ",
                FavouriteAnime = new List<AnimeModel>()
                },
            new UserModel {
                Id = id++,
                Username = "Joker",
                Password = "ewfbd235612estr01_a1wg4357825tjw4sq1_g0dZ",
                FavouriteAnime = new List<AnimeModel>()
                },
            new UserModel {
                Id = id++,
                Username = "Godfather",
                Password = "wwd534y54yestr01_12gwe35211a11_g0dZ",
                FavouriteAnime = new List<AnimeModel>()
                },
            new UserModel {
                Id = id++,
                Username = "Jesus Christ",
                Password = "wegwdr12e23r54yrtstewruji4567r01_a235r23562311_g0dZ",
                FavouriteAnime = new List<AnimeModel>()
                },

            };

        public UserService(IAnimeService animeService)
        {
            _animeService = animeService;

            InitializeUsersWithRandomAnime(3);
        }

        private void InitializeUsersWithRandomAnime(int numberOfAnimePerUser)
        {
            foreach (var user in users)
            {
                for (int i = 0; i < numberOfAnimePerUser; i++)
                {
                    AnimeModel randomAnime = _animeService.GetRandomAnimeToUser().Result;
                    if (randomAnime != null && !user.FavouriteAnime.Any(a => a.Id == randomAnime.Id))
                    {
                        user.FavouriteAnime.Add(randomAnime);
                    }
                }
            }
        }
        public Task<IEnumerable<UserModel>> DeleteUserById(int id)
        {
            users.RemoveAll(x => x.Id == id);
            return Task.FromResult<IEnumerable<UserModel>>(users);
        }

        public Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return Task.FromResult<IEnumerable<UserModel>>(users);
        }

        public Task<UserModel> GetUserById(int id)
        {
            UserModel user = users.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(user);
        }

        public Task<List<UserModel>> UpdateUser(int id, UserModel userModel)
        {
            UserModel updatedUser = users.FirstOrDefault(x => x.Id == id);
            if (updatedUser == null)
            {
                throw new Exception("User not found");
            }
            updatedUser.Username = userModel.Username;
            updatedUser.Password = userModel.Password;
            return Task.FromResult(users);
        }

        public Task<UserModel> AddUser(UserModel userModel)
        {
            userModel.Id = id++;
            users.Add(userModel);
            return Task.FromResult(userModel);

        }
    }
}
