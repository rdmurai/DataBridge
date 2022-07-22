using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBridge.Domain.Entities
{
    public class StorageMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("message")]
        public string? Message { get; set; }

        [BsonElement("date")]
        public DateTime DateTime { get; set; }




    }
}