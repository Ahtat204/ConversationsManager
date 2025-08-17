namespace ChatHistory.Models
{
    public class Conversation
    {
        public required string Id { get; set; }
        public List<Message> Messages { get; set; } = [];

    }

    
}
