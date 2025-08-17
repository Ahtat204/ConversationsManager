namespace ChatHistory.Services
{
    public interface IConversationService
    {
        Task<Conversation> GetAllConversationsAsync();
        Task<Conversation> GetConversationByIdAsync(string id);
        Task<Conversation> CreateConversationAsync(Conversation conversation);
        Task<Conversation> UpdateConversationAsync(string id, Conversation conversation);
        Task<string> DeleteConversationAsync(string id);
    }
}
