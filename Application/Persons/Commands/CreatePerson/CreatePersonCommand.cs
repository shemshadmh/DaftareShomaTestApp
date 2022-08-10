using Application.Persons.Dtos;
using MediatR;

namespace Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommand:IRequest
    {
        public ICollection<PersonDto> Persons { get; set; } = null!;
    }
}
