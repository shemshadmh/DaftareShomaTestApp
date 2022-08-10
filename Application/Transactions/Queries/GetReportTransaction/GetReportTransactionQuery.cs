
using Application.Transactions.Dtos;
using MediatR;

namespace Application.Transactions.Queries.GetReportTransaction
{
    public class GetReportTransactionQuery:IRequest<ICollection<ReportTransactionDto>>
    {
    }
}
