
using Application.Transactions.Dtos;
using FluentValidation;

namespace Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandValidator:AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleForEach(transaction => transaction.Transactions).SetValidator(new CreateTransactionDtoValidator());
        }
    }

    public class CreateTransactionDtoValidator : AbstractValidator<CreateTransactionDto>
    {
        public CreateTransactionDtoValidator()
        {
            RuleFor(transaction => transaction.TransactionId)
                .InclusiveBetween(1, Int32.MaxValue)
                .NotEmpty();

            RuleFor(transaction => transaction.PersonId)
                .InclusiveBetween(1, Int32.MaxValue)
                .NotEmpty();

            RuleFor(transaction => transaction.TransactionDate)
                .NotEmpty();

            RuleFor(transaction => transaction.Price)
                .InclusiveBetween(1, Int32.MaxValue)
                .NotEmpty();
        }
    }
}
