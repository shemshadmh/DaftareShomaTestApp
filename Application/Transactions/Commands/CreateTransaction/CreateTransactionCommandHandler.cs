
using System.Data;
using Application.Interfaces.Application;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandHandler:IRequestHandler<CreateTransactionCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateTransactionCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {

            await ListCheck(request);
            await ExistCheck(request, cancellationToken);

            var transactionList = _mapper.Map<ICollection<Transaction>>(request.Transactions);
            await _dbContext.Transactions.AddRangeAsync(transactionList, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task ExistCheck(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            foreach(var transactionDto in request.Transactions)
            {
                if(await _dbContext.Transactions.AnyAsync(transaction => transaction.Id == transactionDto.TransactionId, cancellationToken))
                    throw new DuplicateNameException("Duplicate record");
            }
        }

        private Task ListCheck(CreateTransactionCommand request)
        {
            if(request.Transactions.GroupBy(x => x.TransactionId).Any(x => x.Skip(1).Any()))
                throw new Exception("Duplicate key in list");
            return Task.CompletedTask;
        }
    }
}
