using ChatHistory.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatHistory.Controllers
{
    /// <summary>
    /// API controller that provides CRUD endpoints for managing <see cref="Conversation"/> entities.
    /// Delegates business logic to the injected <see cref="IConversationService"/>.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ChatHistoryController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatHistoryController"/> class.
        /// </summary>
        /// <param name="conversationService">
        /// The <see cref="IConversationService"/> implementation used to perform conversation operations.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="conversationService"/> is <c>null</c>.
        /// </exception>
        public ChatHistoryController(IConversationService conversationService)
        {
            _conversationService = conversationService 
                ?? throw new ArgumentNullException(nameof(conversationService));
        }

        /// <summary>
        /// Retrieves all conversations.
        /// </summary>
        /// <returns>
        /// A list of all <see cref="Conversation"/> objects.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Conversation>> GetAllConversations()
        {
            return await _conversationService.GetAllConversationsAsync();
        }

        /// <summary>
        /// Retrieves a conversation by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the conversation.</param>
        /// <returns>
        /// The <see cref="Conversation"/> object if found; otherwise, <c>null</c>.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Conversation> GetConversationById(string id)
        {
            return await _conversationService.GetConversationByIdAsync(id);
        }

        /// <summary>
        /// Creates a new conversation.
        /// </summary>
        /// <param name="conversation">The conversation object to create.</param>
        /// <returns>The created <see cref="Conversation"/> object.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Conversation> CreateConversation(Conversation conversation)
        {
            return await _conversationService.CreateConversationAsync(conversation);
        }

        /// <summary>
        /// Updates an existing conversation.
        /// </summary>
        /// <param name="id">The unique identifier of the conversation to update.</param>
        /// <param name="conversation">The updated conversation object.</param>
        /// <returns>The updated <see cref="Conversation"/> object.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Conversation> UpdateConversation(string id, Conversation conversation)
        {
            return await _conversationService.UpdateConversationAsync(id, conversation);
        }

        /// <summary>
        /// Deletes a conversation by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the conversation to delete.</param>
        /// <returns>The ID of the deleted conversation.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string> DeleteConversation(string id)
        {
            return await _conversationService.DeleteConversationAsync(id);
        }
    }
}
