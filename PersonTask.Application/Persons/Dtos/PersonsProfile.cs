using AutoMapper;
using PersonTask.Application.Persons.Commands.UpdatePerson;
using PersonTask.Domain.Entities;

namespace PersonTask.Application.Persons.Dtos;

public class PersonsProfile : Profile
{
    public PersonsProfile()
    {
        CreateMap<UpdatePersonCommand, Person>();

        CreateMap<Person, PersonDto>()
            .ForMember(e => e.Emails, opt => opt.MapFrom(src => src.Emails));
    }
}