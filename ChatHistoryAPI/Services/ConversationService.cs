using ChatHistory.ChatHistoryAPI.Models;
using ChatHistory.ChatHistoryAPI.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatHistory.ChatHistoryAPI.Services;

/// <summary>
/// Service class responsible for handling business logic related to <see cref="Conversation"/>.
/// Delegates CRUD operations to the injected <see cref="IConversationRepository"/>.
/// </summary>
public class ConversationService : IConversationService
{
    /// <summary>
    /// Repository used to perform database operations for conversations.
    /// </summary>
    private readonly IConversationRepository _conversationRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConversationService"/> class.
    /// </summary>
    /// <param name="conversationRepository">
    /// The repository used to access conversation data.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="conversationRepository"/> is null.
    /// </exception>
    public ConversationService(IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository
            ?? throw new ArgumentNullException(nameof(conversationRepository));
    }

    /// <summary>
    /// Creates a new conversation.
    /// </summary>
    /// <param name="conversation">The conversation object to create.</param>
    /// <returns>The created <see cref="Conversation"/> object.</returns>
    public async Task<Conversation> CreateConversationAsync(Conversation conversation)
    {
        return await _conversationRepository.InsertConversationAsync(conversation);
    }

    /// <summary>
    /// Deletes a conversation by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the conversation to delete.</param>
    /// <returns>The ID of the deleted conversation.</returns>
    public async Task<string> DeleteConversationAsync(string id)
    {
        return await _conversationRepository.DeleteConversationAsync(id);
    }

    /// <summary>
    /// Retrieves all conversations.
    /// </summary>
    /// <returns>A list of all <see cref="Conversation"/> objects.</returns>
    public async Task<List<Conversation>> GetAllConversationsAsync()
    {
        return await _conversationRepository.GetAllConversationsAsync();
    }

    /// <summary>
    /// Retrieves a conversation by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the conversation.</param>
    /// <returns>
    /// The <see cref="Conversation"/> object with the specified ID,
    /// or <c>null</c> if not found.
    /// </returns>
    public async Task<Conversation> GetConversationByIdAsync(string id)
    {
        return await _conversationRepository.GetConversationByIdAsync(id);
    }

    /// <summary>
    /// Updates an existing conversation.
    /// </summary>
    /// <param name="id">The unique identifier of the conversation to update.</param>
    /// <param name="conversation">The updated conversation object.</param>
    /// <returns>The updated <see cref="Conversation"/> object.</returns>
    public async Task<Conversation> UpdateConversationAsync(string id, Conversation conversation)
    {
        return await _conversationRepository.UpdateConversationAsync(id, conversation);
    }
}
