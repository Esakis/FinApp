using Store.Models;

public interface ITransactionService
{
    Task AddTransactionAsync(TransactionModel transaction);
    Task<IEnumerable<TransactionModel>> GetAllTransactionsAsync();
    Task<TransactionModel> GetTransactionByIdAsync(int id);
    Task DeleteTransactionAsync(int id);
    Task UpdateTransactionAsync(TransactionModel transaction);
    Task<IEnumerable<TransactionModel>> GetTransactionsByCategoryAsync(int categoryId);
    Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync();
}
