using AutoMapper;

namespace Blog.Profiles;

public class EntryProfile : Profile
{
    public EntryProfile()
    {
        CreateMap<Entities.BlogEntry, Models.BlogEntryDto>();
        CreateMap<Models.BlogEntryForCreationDto, Entities.BlogEntry>();
        CreateMap<Models.BlogEntryForUpdateDto, Entities.BlogEntry>();
        CreateMap<Entities.BlogEntry, Models.BlogEntryForUpdateDto>();
    }
}