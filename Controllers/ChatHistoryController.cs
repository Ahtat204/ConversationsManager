using ChatHistory.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ChatHistory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatHistoryController : ControllerBase
    {
        private readonly IMongoCollection<Conversation>? _conversation;
        public ChatHistoryController(MongoDbService mongoDbService)
        {
             _conversation = mongoDbService.DataBase?.GetCollection<Conversation>("conversations") 
                ?? throw new ArgumentNullException(nameof(mongoDbService), "MongoDB collection 'conversations' not found.");
        }


        [HttpGet]
        public async Task<Conversation> GetAllConversations()
        {
           return await _conversation!.Find(_ => true).FirstOrDefaultAsync() 
                ?? throw new InvalidOperationException("No conversations found.");
        }
    }
}
