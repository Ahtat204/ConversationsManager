namespace ChatHistory.Models
{
    public class Message
    {
        public required Sender Sender { get; set; }
        public required string content { get; set; }
    }

    public enum Sender
    {
        USER,BOT
    }
}
