using MediatR;
using VideoHosting.Core.Entities;

namespace VideoHosting.Core.VideoManagment.Queries;

public record StreamVideoByIdQuery(string Id) : IStreamRequest<BufferedVideo>;