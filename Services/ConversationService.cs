
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace ChatHistory.Services
{
    public  class ConversationService : IConversationService
    {
        private readonly IMongoCollection<Conversation> _conversation;
        private readonly DBSettings _dbSettings;

        public ConversationService(IOptions<DBSettings> conversationsDatabaseSettings)
        {
            _dbSettings= conversationsDatabaseSettings?.Value 
                ?? throw new ArgumentNullException(nameof(conversationsDatabaseSettings), "Database settings cannot be null.");
        }
        public Task<Conversation> CreateConversationAsync(Conversation conversation)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteConversationAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Conversation> GetAllConversationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Conversation> GetConversationByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Conversation> UpdateConversationAsync(string id, Conversation conversation)
        {
            throw new NotImplementedException();
        }
    }
}
