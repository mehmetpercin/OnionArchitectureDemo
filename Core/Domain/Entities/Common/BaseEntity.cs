using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTimeOffset CreatedDate { get; set; }
        public string Creator { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTimeOffset? ModifiedDate { get; set; }
        public string? Modifier { get; set; }
    }
}
