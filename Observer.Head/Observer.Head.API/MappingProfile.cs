using AutoMapper;
using Observer.Head.API.ViewModels;
using Observer.Head.Core.Commands.CreateComment;
using Observer.Head.Core.Commands.UpdateComment;
using Observer.Head.Core.Commands.UploadVideo;
using Observer.Head.Core.Entities;

namespace Observer.Head.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Comment, CommentViewModel>()
            .ForMember(dest => dest.VideoMetadataId, opt => opt.MapFrom(src => src.VideoMetadataId))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.CommentDate, opt => opt.MapFrom(src => src.CommentDate))
            .ReverseMap();

        CreateMap<VideoMetadata, UploadVideoViewModel>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
            .ReverseMap();

        CreateMap<VideoMetadata, UploadVideoViewModel>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
            .ReverseMap();

        ViewModelToCQRS();
        CQRSToEntities();
    }

    public void ViewModelToCQRS()
    {
        CreateMap<CommentViewModel, CreateCommentCommand>()
            .ForMember(dest => dest.VideoMetadataId, opt => opt.MapFrom(src => src.VideoMetadataId))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text));

        CreateMap<CommentViewModel, UpdateCommentCommand>()
            .ForMember(dest => dest.VideoMetadataId, opt => opt.MapFrom(src => src.VideoMetadataId))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text));

        CreateMap<UploadVideoViewModel, UploadVideoCommand>()
            .ForMember(dest => dest.VideoFile, opt => opt.MapFrom(src => src.File))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(name => new Tag { Name = name }).ToList()))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }

    public void CQRSToEntities()
    {
        CreateMap<UploadVideoCommand, VideoMetadata>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
            .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.VideoFile.FileName))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));
    }
}