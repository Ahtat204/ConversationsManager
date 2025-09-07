using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ChatHistory.ChatHistoryAPI.Models;

public class Message
{
    [BsonElement("sender"), BsonRepresentation(BsonType.String)]
    public required Sender Sender { get; set; }
    [BsonElement("message"), BsonRepresentation(BsonType.String)]
    public required string? content { get; set; }
}

public enum Sender
{
    USER,BOT
}
