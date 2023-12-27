using Store.Data;
using Store.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Store.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTransactionAsync(TransactionModel transaction)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == transaction.UserId);
                if (user == null)
                {
                    throw new InvalidOperationException("Użytkownik nie istnieje");
                }

                var categoryExists = await _context.Categories.FirstOrDefaultAsync(c => c.Id == transaction.CategoryId);
                if (categoryExists == null)
                {
                    categoryExists = await _context.Categories.FirstOrDefaultAsync(c => c.Id == 10);
                    transaction.CategoryId = 10;
                }

                if (categoryExists.CategoryIncome)
                {
                    user.Balance += transaction.Amount; 
                }
                else
                {
                    user.Balance -= transaction.Amount;
                }

                _context.Transactions.Add(transaction);


                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public async Task<IEnumerable<TransactionModel>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }


        public async Task<TransactionModel> GetTransactionByIdAsync(int id)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
            if (transaction == null)
            {
                throw new InvalidOperationException("Transakcja nie została znaleziona");
            }
            return transaction;
        }

        public async Task DeleteTransactionAsync(int id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
                if (transaction == null)
                {
                    throw new InvalidOperationException("Transakcja nie istnieje");
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == transaction.UserId);
                if (user == null)
                {
                    throw new InvalidOperationException("Użytkownik nie istnieje");
                }

                var categoryExists = await _context.Categories.FirstOrDefaultAsync(c => c.Id == transaction.CategoryId);
                if (categoryExists == null)
                {
                    categoryExists = await _context.Categories.FirstOrDefaultAsync(c => c.Id == 10);
                    transaction.CategoryId = 10;
                }

                if (categoryExists.CategoryIncome)
                {
                    user.Balance -= transaction.Amount;
                }
                else
                {
                    user.Balance += transaction.Amount;
                }
                _context.Transactions.Remove(transaction);

                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }


        public async Task<IEnumerable<TransactionModel>> GetTransactionsByCategoryAsync(int categoryId)
        {
            return await _context.Transactions
                        .Where(t => t.CategoryId == categoryId)
                        .ToListAsync();
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .ToListAsync();
        }

    }
}
