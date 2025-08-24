using MongoDB.Driver;

namespace ChatHistory.Repositories
{
    /// <summary>
    /// Repository class responsible for performing CRUD operations
    /// on <see cref="Conversation"/> objects in the MongoDB database.
    /// Implements <see cref="IConversationRepository"/>.
    /// </summary>
    internal class ConversationRepository : IConversationRepository
    {
        /// <summary>
        /// MongoDB collection used for storing conversations.
        /// </summary>
        private readonly IMongoCollection<Conversation> _conversation;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConversationRepository"/> class.
        /// Sets up the repository to use the provided <see cref="MongoDbService"/> for database access.
        /// </summary>
        /// <param name="mongoDbService">
        /// The <see cref="MongoDbService"/> instance used to access the MongoDB collection for <see cref="Conversation"/> objects.
        /// Cannot be <c>null</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mongoDbService"/> is <c>null</c>.
        /// </exception>
        public ConversationRepository(MongoDbService mongoDbService)
        {
            _conversation = mongoDbService?.Conversation
                            ?? throw new ArgumentNullException(nameof(mongoDbService), "MongoDB service cannot be null.");
        }


        /// <summary>
        /// Deletes a conversation by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the conversation to delete.</param>
        /// <returns>The ID of the deleted conversation.</returns>
        public async Task<string> DeleteConversationAsync(string id)
        {
            return await _conversation.DeleteOneAsync(c => c.Id == id)
                                      .ContinueWith(t => id);
        }

        /// <summary>
        /// Retrieves all conversations from the database.
        /// </summary>
        /// <returns>A list of all <see cref="Conversation"/> objects.</returns>
        public async Task<List<Conversation>> GetAllConversationsAsync()
        {
            return await _conversation.Find(_c => true).ToListAsync();
        }

        /// <summary>
        /// Retrieves a conversation by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the conversation.</param>
        /// <returns>
        /// The <see cref="Conversation"/> object with the specified ID, or <c>null</c> if not found.
        /// </returns>
        public async Task<Conversation> GetConversationByIdAsync(string id)
        {
            return await _conversation.Find<Conversation>(c => c.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Inserts a new conversation into the database.
        /// </summary>
        /// <param name="conversation">The conversation object to insert.</param>
        /// <returns>The inserted <see cref="Conversation"/> object.</returns>
        public async Task<Conversation> InsertConversationAsync(Conversation conversation)
        {
            return await _conversation.InsertOneAsync(conversation)
                                      .ContinueWith(t => conversation);
        }

        /// <summary>
        /// Updates an existing conversation in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the conversation to update.</param>
        /// <param name="conversation">The updated conversation object.</param>
        /// <returns>The updated <see cref="Conversation"/> object.</returns>
        public async Task<Conversation> UpdateConversationAsync(string id, Conversation conversation)
        {
            return await _conversation.ReplaceOneAsync(c => c.Id == id, conversation).ContinueWith(t => conversation);
        }
    }
}
