using AutoMapper;
using VideoHosting.API.ViewModels;
using VideoHosting.Core.Comments.Commands;
using VideoHosting.Core.Entities;

namespace VideoHosting.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.VideoMetadataId, opt => opt.MapFrom(src => src.VideoMetadataId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.CommentDate, opt => opt.MapFrom(src => src.CommentDate))
                .ReverseMap();

            CreateMap<CreateCommentCommand, CommentViewModel>()
                .ForMember(dest => dest.VideoMetadataId, opt => opt.MapFrom(src => src.VideoMetadataId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ReverseMap();

            CreateMap<UpdateCommentCommand, CommentViewModel>()
                .ForMember(dest => dest.VideoMetadataId, opt => opt.MapFrom(src => src.VideoMetadataId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ReverseMap();

            CreateMap<VideoMetadata, UploadVideoViewModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ReverseMap();
        }
    }
}