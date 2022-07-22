using DataBridge.Domain.Entities;
using DataBridge.Mongo.Interfaces;
using DataBridge.Mongo.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataBridge.Mongo
{
    public class StorageMessageRepository : IStorageMessage

    {

        private readonly IMongoCollection<StorageMessage> dbCollection;

        private readonly FilterDefinitionBuilder<StorageMessage> filterBuilder = Builders<StorageMessage>.Filter;

        public StorageMessageRepository(IOptions<MessageDbSettings> messageDbSettings)
        {
            var mongoClient = new MongoClient(messageDbSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(messageDbSettings.Value.DatabaseName);
            dbCollection = database.GetCollection<StorageMessage>(messageDbSettings.Value.CollectionName);
        }


        public async Task<IReadOnlyCollection<StorageMessage>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<StorageMessage> GetAsync(string id)
        {
            FilterDefinition<StorageMessage> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(StorageMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            await dbCollection.InsertOneAsync(message);

        }

        public async Task UpdateAsync(StorageMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            FilterDefinition<StorageMessage> filter = filterBuilder.Eq(e => e.Id, message.Id);

            await dbCollection.ReplaceOneAsync(filter, message);
        }

        public async Task RemoveAsync(string id)
        {
            FilterDefinition<StorageMessage> filter = filterBuilder.Eq(e => e.Id, id);

            await dbCollection.DeleteOneAsync(filter);
        }

    }
}