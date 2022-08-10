
using Application.Transactions.Dtos;
using MediatR;

namespace Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommand:IRequest
    {
        public ICollection<CreateTransactionDto> Transactions { get; set; } = null!;
    }
}
