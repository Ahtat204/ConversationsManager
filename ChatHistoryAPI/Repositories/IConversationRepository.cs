namespace ChatHistory.ChatHistoryAPI.Repositories;

/// <summary>
/// Defines the contract for performing CRUD operations
/// on <see cref="Conversation"/> objects in the database.
/// </summary>
public interface IConversationRepository
{
    /// <summary>
    /// Retrieves all conversations from the database.
    /// </summary>
    /// <returns>A list of all <see cref="Conversation"/> objects.</returns>
    Task<List<Conversation>> GetAllConversationsAsync();

    /// <summary>
    /// Retrieves a specific conversation by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the conversation.</param>
    /// <returns>
    /// The <see cref="Conversation"/> object with the specified ID, 
    /// or <c>null</c> if no matching conversation is found.
    /// </returns>
    Task<Conversation> GetConversationByIdAsync(string id);

    /// <summary>
    /// Inserts a new conversation into the database.
    /// </summary>
    /// <param name="conversation">The conversation object to insert.</param>
    /// <returns>The inserted <see cref="Conversation"/> object.</returns>
    Task<Conversation> InsertConversationAsync(Conversation conversation);

    /// <summary>
    /// Updates an existing conversation in the database.
    /// </summary>
    /// <param name="id">The unique identifier of the conversation to update.</param>
    /// <param name="conversation">The updated conversation object.</param>
    /// <returns>The updated <see cref="Conversation"/> object.</returns>
    Task<Conversation> UpdateConversationAsync(string id, Conversation conversation);

    /// <summary>
    /// Deletes a conversation from the database by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the conversation to delete.</param>
    /// <returns>The ID of the deleted conversation.</returns>
    Task<string> DeleteConversationAsync(string id);
}
