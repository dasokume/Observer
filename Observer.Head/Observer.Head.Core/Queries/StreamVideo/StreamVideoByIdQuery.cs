using MediatR;
using Observer.Head.Core.Entities;

namespace Observer.Head.Core.Queries.StreamVideo;

public record StreamVideoByIdQuery(string Id) : IStreamRequest<BufferedVideo>;