using DataBridge.Domain.Entities;

namespace DataBridge.Mongo.Interfaces
{
    public interface IStorageMessage
    {
        public Task<IReadOnlyCollection<StorageMessage>> GetAllAsync();


        public Task<StorageMessage> GetAsync(string id);


        public Task CreateAsync(StorageMessage message);


        public Task UpdateAsync(StorageMessage message);


        public Task RemoveAsync(string id);

    }
}
