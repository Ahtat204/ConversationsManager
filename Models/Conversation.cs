using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ChatHistory.Models
{
    public class Conversation
    {
        [BsonId]
        [BsonElement("_id"),BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonElement("title"), BsonRepresentation(BsonType.String)]
        public required string title { get; set; }

        [BsonElement("messages"),BsonRepresentation(BsonType.Array)]
        public List<Message> Messages { get; set; } = new();

    }

    
}
