using MediatR;
using VideoHosting.Core.Entities;

namespace VideoHosting.Core.Video.Queries;

public record StreamVideoByIdQuery(string Id) : IStreamRequest<BufferedVideo>;