using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatHistory.Services
{
    /// <summary>
    /// Provides access to the MongoDB database and its collections for <see cref="Conversation"/> objects.
    /// Responsible only for database connection and collection retrieval.
    /// </summary>
    internal sealed class MongoDbService
    {
        /// <summary>
        /// The MongoDB database instance.
        /// </summary>
        private readonly IMongoDatabase _database;

        /// <summary>
        /// The MongoDB collection used for storing <see cref="Conversation"/> objects.
        /// </summary>
        private readonly IMongoCollection<Conversation> _conversation;

        /// <summary>
        /// Configuration settings for the MongoDB database connection.
        /// </summary>
        private readonly DBSettings _dbSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDbService"/> class.
        /// Sets up the MongoDB client, database, and conversation collection.
        /// </summary>
        /// <param name="dbsettings">
        /// The database settings injected via <see cref="IOptions{DBSettings}"/>.
        /// Must contain connection string, database name, and collection name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="dbsettings"/> is null.
        /// </exception>
        public MongoDbService(IOptions<DBSettings> dbsettings)
        {
            _dbSettings = dbsettings?.Value
                ?? throw new ArgumentNullException(nameof(dbsettings), "Database settings cannot be null.");

            var mongoClient = new MongoClient(_dbSettings.ConnectionString);
            _database = mongoClient.GetDatabase(_dbSettings.DatabaseName);
            _conversation = _database.GetCollection<Conversation>(_dbSettings.CollectionName);
        }

        /// <summary>
        /// Gets the MongoDB database instance.
        /// </summary>
        public IMongoDatabase ConversationsDataBase => _database;

        /// <summary>
        /// Gets the MongoDB collection for <see cref="Conversation"/> objects.
        /// </summary>
        public IMongoCollection<Conversation> Conversation => _conversation;
    }
}
