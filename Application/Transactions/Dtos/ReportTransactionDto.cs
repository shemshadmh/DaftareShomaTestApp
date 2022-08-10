
namespace Application.Transactions.Dtos
{
    public class ReportTransactionDto
    {
        public string Fullname { get; set; } = null!;
        public int PersonId { get; set; }
        public string Date { get; set; } = null!;
        public int Price { get; set; }
        public int Total { get; set; }
    }
}
