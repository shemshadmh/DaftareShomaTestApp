
using Application.Interfaces.Application;
using Application.Transactions.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Queries.GetReportTransaction
{
    public class GetReportTransactionQueryHandler:IRequestHandler<GetReportTransactionQuery, ICollection<ReportTransactionDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetReportTransactionQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<ICollection<ReportTransactionDto>> Handle(GetReportTransactionQuery request, CancellationToken cancellationToken)
        {
            var list = await _dbContext.Transactions
                .Include(transaction => transaction.Person)
                .ProjectTo<ReportTransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var total = 0;
            foreach (var transactionDto in list)
            {
                total += transactionDto.Price;
                transactionDto.Total = total;
            }

            return list;
        }
    }
}
