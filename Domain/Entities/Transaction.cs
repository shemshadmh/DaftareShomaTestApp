
namespace Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Price { get; set; }


        #region Relations

        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;

        #endregion
    }
}
