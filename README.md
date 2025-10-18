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

"Authentication":{
"ApiKey":"put your Apikey here"
},
"ChatBotConversationsDataBase": {
"ConnectionString": "mongodb+srv://username:password@cluster0.mongodb.net", 
"DatabaseName": "ChatBotConversations",
"CollectionName": "Conversation"
}
the request header name is x-api-key, its value is the value of ApiKey field in the appsettings.json file 

Once configured, the API will automatically connect to your database and allow you to manage conversation history.

to test the API , you can either clone this repository or ,if you have docker installed , simply run :
"docker pull lahcen3ahtat/conversations-manager:1.2 " 
Then, if the image is successfully built, run :
docker run -d -p 8080:80 lahcen3ahtat/conversations-manager 

example of a json object :
{
"title": "greeting message",
"messages": [
{
"sender": 0,
"content": "Hello, how are you?"
},
{
"sender": 1,
"content": "I’m doing great, thank you! How can I help you today?"
},
{
"sender": 0,
"content": "Can you tell me a joke?"
},
{
"sender": 1,
"content": "Sure! Why don’t skeletons fight each other? Because they don’t have th…"
}
]
}

