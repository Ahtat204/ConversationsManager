using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatHistory.ChatHistoryAPI.Models;


/// <summary>
/// 
/// </summary>
public class Conversation
{
    
    [BsonId]
    [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }=string.Empty;

    [BsonElement("title"), BsonRepresentation(BsonType.String)]
    public required string title { get; set; }

    [BsonElement("messages")]
    public List<Message> Messages { get; set; } = [];

}


