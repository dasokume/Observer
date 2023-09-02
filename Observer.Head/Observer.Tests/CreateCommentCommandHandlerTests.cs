using FluentAssertions;
using Moq;
using Observer.Head.Core.Commands.CreateComment;
using Observer.Head.Core.Entities;
using Observer.Head.Core.Interfaces;
using Xunit;

namespace Observer.Head.Tests;

public class CreateCommentCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_CreatedComment()
    {
        // Arrange
        var commentRepositoryMock = new Mock<ICommentRepository>();
        var handler = new CreateCommentCommandHandler(commentRepositoryMock.Object);
        Comment? actualComment = null;

        var command = new CreateCommentCommand()
        {
            VideoMetadataId = Guid.NewGuid().ToString(),
            Text = "Test comment text"
        };

        commentRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Comment>()))
            .Callback<Comment>(comment => actualComment = comment);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        actualComment.Should().NotBeNull();
        actualComment!.VideoMetadataId.Should().Be(command.VideoMetadataId);
        actualComment!.Text.Should().Be(command.Text);
    }
}