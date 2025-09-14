using ChatHistory.ChatHistoryAPI.Models;
using ChatHistory.ChatHistoryAPI.Repositories;
using ChatHistory.ChatHistoryAPI.Services;
using Moq;
using Xunit.Abstractions;

namespace ChatHistoryTest.Services;

/// <summary>
/// Unit tests for the <see cref="ConversationService"/> class.
/// Uses Moq to mock the repository and xUnit for assertions.
/// </summary>
public class ConversationServiceTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Mock<IConversationRepository> _mockRepo;
    private readonly ConversationService _conversationService;

    /// <summary>
    /// Initializes a new instance of <see cref="ConversationServiceTest"/>.
    /// Sets up the mock repository and the service under test.
    /// </summary>
    /// <param name="testOutputHelper">Used for writing test output if needed.</param>
    public ConversationServiceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _mockRepo = new Mock<IConversationRepository>();
        _conversationService = new ConversationService(_mockRepo.Object);
    }

    /// <summary>
    /// Tests that <see cref="ConversationService.GetAllConversationsAsync"/>
    /// returns a list of conversations as expected.
    /// </summary>
    [Fact]
    public async Task GetAllConversationsAsync_ShouldReturnListOfConversations()
    {
        // Arrange: create a mock list of conversations
        var mockConversations = new List<Conversation>
        {
            new Conversation { Id = "1", title = "Conversation 1" },
            new Conversation { Id = "2", title = "Conversation 2" }
        };

        _mockRepo.Setup(repo => repo.GetAllConversationsAsync())
            .ReturnsAsync(mockConversations);

        // Act: call the service method
        var result = await _conversationService.GetAllConversationsAsync();

        // Assert: verify the result matches expectations
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Conversation 1", result[0].title);
        Assert.Equal("Conversation 2", result[1].title);
    }

    /// <summary>
    /// Tests that <see cref="ConversationService.GetConversationByIdAsync"/>
    /// returns the correct conversation for a given Id.
    /// </summary>
    [Fact]
    public async Task GetConversationAsync_ShouldReturnConversation()
    {
        // Arrange: create a mock conversation
        var mockConversation = new Conversation { Id = "1", title = "Conversation 1" };

        _mockRepo.Setup(repo => repo.GetConversationByIdAsync(mockConversation.Id))
            .ReturnsAsync(mockConversation);

        // Act: call the service method
        var result = await _conversationService.GetConversationByIdAsync(mockConversation.Id);

        // Assert: verify the result
        Assert.NotNull(result);
        Assert.Equal("Conversation 1", result.title);

        // Verify that the repository method was called exactly once
        _mockRepo.Verify(repo => repo.GetConversationByIdAsync(mockConversation.Id), Times.Once);
    }

    /// <summary>
    /// Tests that <see cref="ConversationService.CreateConversationAsync"/>
    /// correctly inserts a new conversation and returns it with an assigned Id.
    /// </summary>
    [Fact]
    public async Task CreateConversationAsync_ShouldReturnCreatedConversation()
    {
        // Arrange: define a new conversation to insert and the expected created conversation
        var newConversation = new Conversation { title = "New Conversation" };
        var createdConversation = new Conversation { Id = "1", title = "New Conversation" };

        _mockRepo.Setup(repo => repo.InsertConversationAsync(It.IsAny<Conversation>()))
            .ReturnsAsync(createdConversation);

        // Act: call the service method
        var result = await _conversationService.CreateConversationAsync(newConversation);

        // Assert: verify the returned conversation matches the expected values
        Assert.NotNull(result);
        Assert.Equal("1", result.Id);
        Assert.Equal("New Conversation", result.title);

        // Verify that InsertConversationAsync was called exactly once
        _mockRepo.Verify(repo => repo.InsertConversationAsync(It.IsAny<Conversation>()), Times.Once);
    }
}