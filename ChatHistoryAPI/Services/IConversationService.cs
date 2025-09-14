namespace ChatHistory.ChatHistoryAPI.Services;

/// <summary>
/// Defines the contract for conversation-related business logic.
/// Provides methods to perform CRUD operations on <see cref="Conversation"/> objects.
/// </summary>
public interface IConversationService
{
    /// <summary>
    /// Retrieves all conversations.
    /// </summary>
    /// <returns>A list of all <see cref="Conversation"/> objects.</returns>
    Task<List<Conversation>> GetAllConversationsAsync();

    /// <summary>
    /// Retrieves a conversation by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the conversation.</param>
    /// <returns>
    /// The <see cref="Conversation"/> object with the specified ID,
    /// or <c>null</c> if not found.
    /// </returns>
    Task<Conversation> GetConversationByIdAsync(string id);

    /// <summary>
    /// Creates a new conversation.
    /// </summary>
    /// <param name="conversation">The conversation object to create.</param>
    /// <returns>The created <see cref="Conversation"/> object.</returns>
    Task<Conversation> CreateConversationAsync(Conversation conversation);

    /// <summary>
    /// Updates an existing conversation.
    /// </summary>
    /// <param name="id">The unique identifier of the conversation to update.</param>
    /// <param name="conversation">The updated conversation object.</param>
    /// <returns>The updated <see cref="Conversation"/> object.</returns>
    Task<Conversation> UpdateConversationAsync(string id, Conversation conversation);

    /// <summary>
    /// Deletes a conversation by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the conversation to delete.</param>
    /// <returns>The ID of the deleted conversation.</returns>
    Task<string> DeleteConversationAsync(string id);
}
