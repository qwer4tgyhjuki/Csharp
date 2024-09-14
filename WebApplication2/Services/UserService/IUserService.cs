using WebApplication2.Models;

namespace WebApplication2.Services.UserService
{
    public interface IUserService
    {
        public Task<UserModel> GetUserById(int id);
        public Task<IEnumerable<UserModel>> GetAllUsers();
        public Task<List<UserModel>> UpdateUser(int id, UserModel userModel);
        public Task<IEnumerable<UserModel>> DeleteUserById(int id);

        public Task<UserModel> AddUser(UserModel userModel);
    }
}
