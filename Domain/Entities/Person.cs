
namespace Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Family { get; set; } = null!;


        #region Relations

        public ICollection<Transaction>? Transactions { get; set; }

        #endregion
    }
}
