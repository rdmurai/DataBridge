using DataBridge.Domain.Entities;
using DataBridge.Infrastructure.Repository.Interfaces;

namespace DataBridge.Infrastructure.Repository
{
    public class UserRepository : IUser
    {
        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(string username, string password)
        {
            return new User() { Id = 1, Username = "teste", Password = "123", Role = "manager" };
        }

        public Task PostAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task PutAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
