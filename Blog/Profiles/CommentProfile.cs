using AutoMapper;

namespace Blog.Profiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Entities.Comment, Models.CommentDto>();
        CreateMap<Models.CommentForCreationDto, Entities.Comment>();
        CreateMap<Models.CommentForUpdateDto, Entities.Comment>();
        CreateMap<Entities.Comment, Models.CommentForUpdateDto>();

    }
}