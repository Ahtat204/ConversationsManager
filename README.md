Gemini Backend API – Conversation History

This project provides a backend API for the Gemini project, allowing you to store and retrieve conversation history.

Getting Started

Create an appsettings.json file in the project root.

Add the following configuration for your MongoDB connection:

"ChatBotConversationsDataBase": {
"ConnectionString": "<YOUR_MONGODB_CONNECTION_STRING>",
"DatabaseName": "ChatBotConversations",
"CollectionName": "Conversation"
}


ConnectionString: Replace <YOUR_MONGODB_CONNECTION_STRING> with your actual MongoDB connection string.

DatabaseName: The name of the database where conversation data will be stored.

CollectionName: The collection that will store individual conversations.

How It Works

The backend exposes endpoints to create, retrieve, and manage conversations.

The service interacts with MongoDB via the connection string provided in the configuration.

Example
"ChatBotConversationsDataBase": {
"ConnectionString": "mongodb+srv://username:password@cluster0.mongodb.net",
"DatabaseName": "ChatBotConversations",
"CollectionName": "Conversation"
}


Once configured, the API will automatically connect to your database and allow you to manage conversation history.