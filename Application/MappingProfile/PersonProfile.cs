using Application.Features.Person.Dto;
using AutoMapper;
using Domain.Entitites;

namespace Application.MappingProfile;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonDto>()
            .ReverseMap()
            .ForMember(dest => dest.Emails, opt => opt.Ignore());

        CreateMap<Email, EmailDto>()
            .ReverseMap();
    }
}