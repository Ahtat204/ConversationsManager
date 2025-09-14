using ChatHistory.ChatHistoryAPI.Controllers;
using ChatHistory.ChatHistoryAPI.Models;
using ChatHistory.ChatHistoryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit.Abstractions;


namespace ChatHistoryTest.Controllers;

/// <summary>
/// Unit tests for <see cref="ChatHistoryController"/> 
/// focusing on conversation-related endpoints.
/// Uses Moq to simulate <see cref="IConversationService"/> behavior.
/// </summary>
public class ConversationControllerTest
{
    #pragma warning disable IDE0052
    /// <summary>
    /// Helper for writing test output.
    /// </summary>
    private readonly ITestOutputHelper _testOutputHelper;
    #pragma warning restore IDE0052
    /// <summary>
    /// The controller instance under test.
    /// </summary>
    private readonly ChatHistoryController _controller;

    /// <summary>
    /// Mock of the conversation service used to simulate service behavior.
    /// </summary>
    private readonly Mock<IConversationService> _mockConversationService;

    /// <summary>
    /// Initializes a new instance of <see cref="ConversationControllerTest"/>.
    /// Sets up the mocked conversation service and the controller.
    /// </summary>
    /// <param name="testOutputHelper">Xunit test output helper for logging.</param>
    public ConversationControllerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _mockConversationService = new();
        _controller = new ChatHistoryController(_mockConversationService.Object);
    }

    #region HappyPath
    /// <summary>
    /// Tests that <see cref="ChatHistoryController.GetConversationById"/> 
    /// returns an <see cref="OkObjectResult"/> with the expected <see cref="Conversation"/>.
    /// </summary>
    [Fact]
    public async Task GetConversationById_ReturnConversation()
    {
        // Arrange
        var conversation = new Conversation { Id = "1", title = "Converstion1",  Messages =
        [
            new(sender: Sender.USER, content: "Hello, how are you?"),
            new(sender: Sender.BOT, content: "I'm good, thank you!,how can I help you")
        ] };
        _mockConversationService.Setup(serv => serv.GetConversationByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(conversation);
        // Act
        var result = await _controller.GetConversationById(conversation.Id);
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result); // unwrap ActionResult
        var returnedConversation = Assert.IsType<Conversation>(okResult.Value);
        Assert.Equal(conversation.Id, returnedConversation.Id);
        Assert.Equal(conversation.title, returnedConversation.title);
        Assert.Equal(conversation.Messages.Count, returnedConversation.Messages.Count);
        Assert.Equal(conversation.Messages[0].content, returnedConversation.Messages[0].content);
        Assert.Equal(conversation.Messages[1].content, returnedConversation.Messages[1].content);
    }

    /// <summary>
    /// Tests that <see cref="ChatHistoryController.CreateConversation"/> 
    /// returns a <see cref="CreatedAtActionResult"/> containing the created <see cref="Conversation"/> 
    /// and the correct route values.
    /// </summary>
    [Fact]
    public async Task CreateConversation_ReturnsCreatedConversation()
    {
        var conversation = new Conversation { Id = "1", title = "Converstion1",  Messages =
        [
            new(sender: Sender.USER, content: "Hello, how are you?"),
            new(sender: Sender.BOT, content: "I'm good, thank you!,how can I help you")
        ] };
        _mockConversationService
            .Setup(serv => serv.CreateConversationAsync(It.IsAny<Conversation>()))
            .ReturnsAsync(conversation);
        var result = await _controller.CreateConversation(conversation);
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(_controller.GetConversationById), createdResult.ActionName);
        Assert.Equal(conversation.Id, createdResult.RouteValues?["id"]);
        var returnedConversation = Assert.IsType<Conversation>(createdResult.Value);
        Assert.Equal(conversation.Id, returnedConversation.Id);
        Assert.Equal(conversation.title, returnedConversation.title);
        Assert.Equal(conversation.Messages.Count, returnedConversation.Messages.Count);
        Assert.Equal(conversation.Messages[0].content, returnedConversation.Messages[0].content);
        Assert.Equal(conversation.Messages[1].content, returnedConversation.Messages[1].content);
    }
    
    /// <summary>
    /// Tests that <see cref="ChatHistoryController.UpdateConversation"/> returns an
    /// <see cref="OkObjectResult"/> with the updated <see cref="Conversation"/> 
    /// </summary>
    [Fact]
    public async Task UpdateConversation_returnsUpdatedConversation()
    {
        var id = "1";
        var updatedConversation = new Conversation { Id = id, title = "Converstion2" ,  Messages =
        [
            new(sender: Sender.USER, content: "Hello, how are you?"),
            new(sender: Sender.BOT, content: "I'm good, thank you!,how can I help you")
        ]};
        _mockConversationService.Setup(serv => serv.UpdateConversationAsync(id, updatedConversation))
            .ReturnsAsync(updatedConversation);
        var result = await _controller.UpdateConversation(id, updatedConversation);
        var createdResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedConversation = Assert.IsType<Conversation>(createdResult.Value);
        Assert.Equal(id, returnedConversation.Id);
        Assert.Equal(updatedConversation.title, returnedConversation.title);
        
    }

    /// <summary>
    /// Tests that the <see cref="ChatHistoryController.DeleteConversation"/> returns the id of the deleted <see cref="Conversation"/>
    /// </summary>
    [Fact]
    public async Task DeleteConversation_returnsId()
    {
        var id = "1";
        _mockConversationService.Setup(serv => serv.DeleteConversationAsync(id)).ReturnsAsync(id);
        var result = await _controller.DeleteConversation(id);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedId = Assert.IsType<string>(okResult.Value);
        Assert.Equal(id, returnedId);
        
    }
    #endregion

    #region EdgeCases

    [Fact]
    public async Task GetConversationById_ReturnsNotFound()
    {
        var id = "999";
        _mockConversationService.Setup(serv => serv.GetConversationByIdAsync(id))!
            .ReturnsAsync((Conversation?)null);
        var result = await _controller.GetConversationById(id);
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateConversation_ReturnsBadRequestWhenConversationIsNull()
    {
        var conversation = new Conversation  { 
            Id = "102034",
            title = null, 
            Messages =
            [
                new(sender: Sender.USER, content: "Hello, how are you?"),
                new(sender: Sender.BOT, content: "I'm good, thank you!,how can I help you")
            ]
        };
        _mockConversationService.Setup(serv => serv.CreateConversationAsync(It.IsAny<Conversation>())).ReturnsAsync(conversation);
        var result = await _controller.CreateConversation(conversation);
        var badRequestResult = Assert.IsType<BadRequestResult>(result));
    }
    #endregion

}