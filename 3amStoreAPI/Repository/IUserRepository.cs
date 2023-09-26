using _3amStoreAPI.Model;

namespace _3amStoreAPI.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetUsers();
        Task<UserModel> GetUserById(int User_id);
        Task<UserModel> AddUser(UserModel User);
        Task<UserModel> UpdateUser(UserModel User);
        Task<bool> DeleteUser(int User_id);
    }
}
