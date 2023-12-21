namespace Store.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TransactionModel> Transactions { get; set; }
    }
}
