using FluentAssertions;
using Moq;
using Observer.Head.Core.Commands.DeleteComment;
using Observer.Head.Core.Interfaces;
using Xunit;

namespace Observer.Head.Tests;

public class DeleteCommentCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_ReturnsDeletedComment()
    {
        // Arrange
        var commentRepositoryMock = new Mock<ICommentRepository>();
        var handler = new DeleteCommentCommandHandler(commentRepositoryMock.Object);
        var command = new DeleteCommentCommand("1");

        commentRepositoryMock.Setup(x => x.DeleteAsync(command.Id)).ReturnsAsync(true);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }
}