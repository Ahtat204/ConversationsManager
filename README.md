## 🚀 Gemini Backend API – Conversation History

This backend API service is designed to **store and retrieve conversation history** for your Gemini project, leveraging a **MongoDB** database for persistence.

-----

## 🛠️ Getting Started: Configuration

The API requires an `appsettings.json` file in the project root to configure the database connection and API key for authentication.

### 1\. `appsettings.json` Structure

Create an `appsettings.json` file and add the following configuration sections. **Replace the placeholder values** with your actual credentials.

```json
{
  "Authentication": {
    "ApiKey": "put your Apikey here"
  },
  "ChatBotConversationsDataBase": {
    "ConnectionString": "mongodb+srv://username:password@cluster0.mongodb.net",
    "DatabaseName": "ChatBotConversations",
    "CollectionName": "Conversation"
  }
}
```

### 2\. Configuration Details

| Setting | Purpose | Example Value |
| :--- | :--- | :--- |
| **`ConnectionString`** | Your actual **MongoDB connection string**. | `"mongodb+srv://user:pass@cluster0.mongodb.net"` |
| **`DatabaseName`** | The database name where conversations will be stored. | `"ChatBotConversations"` |
| **`CollectionName`** | The collection that will store individual conversations. | `"Conversation"` |
| **`ApiKey`** | The key used for API authentication. | `"your-secret-api-key"` |

-----

## 🔒 API Authentication

All requests to the API must include the **`ApiKey`** in the request header for authentication.

  * **Header Name:** `x-api-key`
  * **Header Value:** The value of the **`ApiKey`** field from your `appsettings.json`.

-----

## 💡 How It Works

The service exposes a set of endpoints that allow you to **create, retrieve, and manage conversations**. It automatically connects to your MongoDB instance using the provided configuration, enabling seamless history management.

### Example Conversation Data Format

Conversations are stored as JSON objects. The `messages` array contains objects where `sender` differentiates between user (`0`) and bot (`1`) messages.

```json
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
```

-----

## 🐳 Running with Docker

The easiest way to test the API is by using Docker and `docker-compose`, which sets up both the API service and a local MongoDB instance.

### 1\. Create `docker-compose.yml`

Create a file named `docker-compose.yml` and paste the following content:

```yaml
version: '3.9'

services:
  # --- API Service Configuration ---
  api:
    image: lahcen3ahtat/conversations-manager:1.2 # The API's Docker image
    ports:
      - "8080:80" # Map host port 8080 to container port 80
    environment:
      # These environment variables override the appsettings.json for Docker
      - MONGO_CONNECTION=mongodb://db:27017 # Connects to the 'db' service defined below
      - ASPNETCORE_URLS=http://+:80
      - ASPNETCORE_ENVIRONMENT=Development
      - MongoDB__DatabaseName=ChatBotConversations
      - MongoDB__CollectionName=Conversation
    depends_on:
      - db # Ensure MongoDB starts before the API
    networks:
      - app-network

  # --- MongoDB Service Configuration ---
  db:
    image: mongo:7.0 # Official MongoDB image
    container_name: mongodb
    restart: always
    healthcheck:
      test: ["CMD", "mongosh", "--quiet", "--eval", "db.runCommand({ ping: 1 }).ok"]
      interval: 10s
      timeout: 5s
      retries: 5
      start_period: 5s
    ports:
      - "27017:27017" # Map host port 27017 to container port 27017 (for external access if needed)
    volumes:
      - mongo-data:/data/db # Persist MongoDB data
    networks:
      - app-network

# --- Docker Volume for Data Persistence ---
volumes:
  mongo-data:

# --- Docker Network for Service Communication ---
networks:
  app-network:
```

### 2\. Run the Services

Execute the following command in the same directory as your `docker-compose.yml`:

```bash
docker-compose up -d
```

### 3\. Access the API

Once the services are running, the API will be accessible at:

**`http://localhost:8080/`**

GET **http://localhost:8080/api/ChatHistory**  returns all conversations

GET **http://localhost:8080/api/ChatHistory{id}** return a specific Conversation

POST **http://localhost:8080/api/ChatHistory** create a conversation ,you can use the ""Example Conversation Data Format"" 
