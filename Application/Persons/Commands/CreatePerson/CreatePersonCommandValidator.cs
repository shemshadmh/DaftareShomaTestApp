
using Application.Persons.Dtos;
using FluentValidation;

namespace Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandValidator:AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleForEach(person => person.Persons).SetValidator(new PersonDtoValidator());
        }
    }

    public class PersonDtoValidator : AbstractValidator<PersonDto>
    {
        public PersonDtoValidator()
        {
            this.RuleFor(person => person.PersonId)
                .InclusiveBetween(1,Int32.MaxValue)
                .NotEmpty();

            this.RuleFor(person => person.Name)
                .MaximumLength(30)
                .NotEmpty();

            this.RuleFor(person => person.Family)
                .MaximumLength(30)
                .NotEmpty();
        }
    }
}
