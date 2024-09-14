using System.Runtime.CompilerServices;
using WebApplication2.Models;
using WebApplication2.Services.AnimeService;
using WebApplication2.Services.AuthService;

namespace WebApplication2.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IAnimeService _animeService;
        public readonly IAuthService _authService;
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
                Password = "no_hash",
                FavouriteAnime = new List<AnimeModel>()
            },
            new UserModel {
                Id = id++,
                Username = "Kratos",
                Password = "stalker123",
                FavouriteAnime = new List<AnimeModel>()
                },
            new UserModel {
                Id = id++,
                Username = "DOOM Slayer",
                Password = "doom12345",
                FavouriteAnime = new List<AnimeModel>()
                },
            new UserModel {
                Id = id++,
                Username = "Postal_man",
                Password = "1234",
                FavouriteAnime = new List<AnimeModel>()
                },
            new UserModel {
                Id = id++,
                Username = "Batman",
                Password = "123456",
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

        private void InitializeUsersWithHashedPassword()
        {
            foreach (var user in users)
            {
                _authService.SetUserPasswordHash(user, user.Password);
            }
        }

        public UserService(IAuthService authService)
        {
            _authService = authService;
            InitializeUsersWithHashedPassword();
            //_animeService = animeService;

            //InitializeUsersWithRandomAnime(3);
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

        public Task<bool> GetUserByName(LoginModel loginModel)
        {
            
            var user = users.FirstOrDefault(u => u.Username == loginModel.Username);
            if(user != null && _authService.VerifyPassword(user, loginModel.Password))
            {
                user.LastLogin = DateTime.Now;
                return Task.FromResult(true);
            }
            user.AttemptsLogin++;
            return Task.FromResult(false);
        }

        public bool GetUsername(string username, string email)
        {
            var user = users.FirstOrDefault(u => u.Username == username);
            if(user.Username != null || user.Email != null)
            {
                return true;
            }
            return false;
        }
    }
}
