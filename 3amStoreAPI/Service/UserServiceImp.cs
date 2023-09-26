using _3amStoreAPI.Model;
using _3amStoreAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace _3amStoreAPI.Service
{
    public class UserServiceImp : IUserRepository
    {
        private readonly DatabaseContext _dbContext;

        public UserServiceImp(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        } 

        public async Task<UserModel> AddUser(UserModel User)
        {
            UserModel user = await _dbContext.Users.FirstOrDefaultAsync(u => u.email.Equals(User.email));
            if (user == null)
            {
                await _dbContext.Users.AddAsync(User);
                await _dbContext.SaveChangesAsync();
                return User;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteUser(int User_id)
        {
            UserModel user = await _dbContext.Users.FirstOrDefaultAsync(u => u.user_id.Equals(User_id));
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<UserModel> GetUserById(int User_id)
        {
            UserModel user = await _dbContext.Users.FirstOrDefaultAsync(u => u.user_id.Equals(User_id));
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> UpdateUser(UserModel User)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.user_id.Equals(User.user_id));
            if (user != null)
            {
                _dbContext.Entry(User).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return User;
            }
            else
            {
                return null;
            }
        }
    }
}
