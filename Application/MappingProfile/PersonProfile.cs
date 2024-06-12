using Application.Features.Person.Dto;
using Domain.Entitites;

namespace Application.MappingProfile;

using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails));

        CreateMap<PersonDto, Person>()
         .ForMember(dest => dest.Id, opt => opt.Ignore())
         .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
         .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
         .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
         .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails.Select(e => new EmailDto { Id = e.Id, EmailAddress = e.EmailAddress })))
         .AfterMap((src, dest) =>
         {
             dest.Emails.RemoveAll(e => !src.Emails.Any(eDto => eDto.Id == e.Id));
         });

        CreateMap<Email, EmailDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress));

        CreateMap<EmailDto, Email>()
            .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress));

        CreateMap<CreatePersonDto, Person>()
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails.Select(e => new Email { EmailAddress = e.Email })));

        CreateMap<CreateEmailDto, Email>()
           .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email));
    }
}