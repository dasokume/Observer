using MediatR;
using Observer.Head.Core.Entities;

namespace Observer.Head.Core.Video.Queries;

public record StreamVideoByIdQuery(string Id) : IStreamRequest<BufferedVideo>;