
namespace Application.Transactions.Dtos
{
    public class CreateTransactionDto
    {
        public int TransactionId { get; set; }
        public string TransactionDate { get; set; } = null!;
        public int Price { get; set; }
        public int PersonId { get; set; }
    }
}
