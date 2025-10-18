global using ChatHistory.ChatHistoryAPI.Models;
global using ChatHistory.ChatHistoryAPI.Services;
global using DBSettings = ChatHistory.ChatHistoryAPI.ChatBotConversationDataBaseSettings;
using ChatHistory.ChatHistoryAPI.Authentication;
using ChatHistory.ChatHistoryAPI.Repositories;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DBSettings>(builder.Configuration.GetSection("ChatBotConversationsDataBase"));
builder.Services.AddScoped<IConversationService, ConversationService>();
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddSingleton<MongoDbService>();
//builder.Services.AddSingleton<MongoDbService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseMiddleware<ApiKeyAuthMiddleware>();
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
