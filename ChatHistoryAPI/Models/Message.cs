using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ChatHistory.ChatHistoryAPI.Models;
/// <summary>
/// 
/// </summary>
public class Message
{
    public Message()
    {
    }

    [SetsRequiredMembers]
    public Message(Sender sender, string? content)
    {
        Sender = sender;
        this.content = content;
    }

    [BsonElement("sender"), BsonRepresentation(BsonType.String)]
    public required Sender Sender { get; set; }
    [BsonElement("message"), BsonRepresentation(BsonType.String)]
    public required string? content { get; set; }
}

public enum Sender
{
    USER, BOT
}
