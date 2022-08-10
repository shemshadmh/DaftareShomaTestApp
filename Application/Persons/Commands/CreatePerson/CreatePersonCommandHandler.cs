
using Application.Interfaces.Application;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandHandler:IRequestHandler<CreatePersonCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreatePersonCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            await ListCheck(request);
            await ExistCheck(request, cancellationToken);

            var person = _mapper.Map<ICollection<Person>>(request.Persons);
            await _dbContext.Persons.AddRangeAsync(person, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task ExistCheck(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            foreach (var personDto in request.Persons)
            {
                if (await _dbContext.Persons.AnyAsync(person => person.Id == personDto.PersonId, cancellationToken))
                    throw new Exception("Duplicate record");
            }
        }

        private Task ListCheck(CreatePersonCommand request)
        {
            if (request.Persons.GroupBy(x => x.PersonId).Any(x => x.Skip(1).Any()))
                throw new Exception("Duplicate key in list");
            return Task.CompletedTask;
        }
    }
}
