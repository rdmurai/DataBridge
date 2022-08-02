using DataBridge.Domain.Entities;

namespace DataBridge.Infrastructure.Repository.Interfaces
{
    public interface IUser
    {
        public Task<IReadOnlyCollection<User>> GetAllAsync();
        public Task<User> GetAsync(string username, string password);
        public Task PutAsync(User user);
        public Task PostAsync(User user);
        public Task DeleteAsync(User user);
    }
}
