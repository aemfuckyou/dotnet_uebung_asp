using AutoMapper;

namespace Blog.Profiles;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<Entities.Author, Models.AuthorDto>();
    }
}