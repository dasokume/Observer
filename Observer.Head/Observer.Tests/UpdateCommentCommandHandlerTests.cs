using FluentAssertions;
using Moq;
using Observer.Head.Core.Comments.Commands;
using Observer.Head.Core.Core.Comments.CommandHandlers;
using Observer.Head.Core.Entities;
using Observer.Head.Core.Interfaces;
using Xunit;

namespace Observer.Head.Tests;

public class UpdateCommentCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_ReturnsUpdatedComment()
    {
        // Arrange
        var commentRepositoryMock = new Mock<ICommentRepository>();
        var handler = new UpdateCommentCommandHandler(commentRepositoryMock.Object);
        Comment? actualComment = null;

        var command = new UpdateCommentCommand()
        {
            VideoMetadataId = Guid.NewGuid().ToString(),
            Text = "Updated comment text"
        };

        commentRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Comment>(), It.IsAny<string>()))
            .Callback<Comment, string>((comment, id) => actualComment = comment);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        actualComment.Should().NotBeNull();
        actualComment!.VideoMetadataId.Should().Be(command.VideoMetadataId);
        actualComment!.Text.Should().Be(command.Text);
    }
}