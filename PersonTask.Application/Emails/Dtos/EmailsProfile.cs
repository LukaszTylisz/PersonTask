using AutoMapper;
using PersonTask.Application.Emails.Commands.CreateEmail;
using PersonTask.Domain.Entities;

namespace PersonTask.Application.Emails.Dtos;

public class EmailsProfile : Profile
{
    public EmailsProfile()
    {
        CreateMap<CreatePersonEmailCommand, PersonEmail>();
        CreateMap<PersonEmail, PersonEmailDto>();
    }
}