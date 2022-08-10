
using Application.Persons.Commands.CreatePerson;
using Application.Persons.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonDto, Person>()
                .ForMember(dest =>
                    dest.Id, opt =>
                    opt.MapFrom(src => src.PersonId));
        }
    }
}
