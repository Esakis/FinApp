using Store.Data;
using Store.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            var userExists = await _context.Users.AnyAsync(u => u.Id == transaction.UserId);
            if (!userExists)
            {
                throw new InvalidOperationException("Użytkownik nie istnieje");
            }

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == transaction.CategoryId);
            if (!categoryExists)
            {
                if (!await _context.Categories.AnyAsync(c => c.Id == 10))
                {
                    throw new InvalidOperationException("Kategoria domyślna (ID: 10) nie istnieje w bazie danych");
                }
                transaction.CategoryId = 10;
            }

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<TransactionModel>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }


        public async Task<TransactionModel> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateTransactionAsync(TransactionModel transaction)
        {
            var existingTransaction = await _context.Transactions.FindAsync(transaction.Id);
            if (existingTransaction != null)
            {
                existingTransaction.Amount = transaction.Amount;
                existingTransaction.Date = transaction.Date;
                existingTransaction.Description = transaction.Description;
                existingTransaction.CategoryId = transaction.CategoryId;
                existingTransaction.UserId = transaction.UserId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<TransactionSummary> GetTransactionSummaryForUserAsync(int userId)
        {
            var userTransactions = _context.Transactions.Where(t => t.UserId == userId);

            var transactionSummary = new TransactionSummary
            {
                TotalIncome = await userTransactions.Where(t => t.Amount > 0).SumAsync(t => t.Amount),
                TotalExpense = await userTransactions.Where(t => t.Amount < 0).SumAsync(t => t.Amount),
                TotalTransactionsCount = await userTransactions.CountAsync(),
                HighestTransactionAmount = await userTransactions.MaxAsync(t => t.Amount),
                LowestTransactionAmount = await userTransactions.MinAsync(t => t.Amount),
                MostRecentTransactionDate = await userTransactions.MaxAsync(t => t.Date)
            };

            var categoryTotals = await userTransactions
                .GroupBy(t => t.CategoryId)
                .Select(group => new { CategoryId = group.Key, TotalAmount = group.Sum(t => t.Amount) })
                .ToListAsync();

            var categoryNames = await _context.Categories
                .Where(c => categoryTotals.Select(ct => ct.CategoryId).Contains(c.Id))
                .ToDictionaryAsync(c => c.Id, c => c.Name);

            transactionSummary.TotalByCategory = categoryTotals
                .ToDictionary(
                    k => categoryNames.ContainsKey(k.CategoryId) ? categoryNames[k.CategoryId] : "Nieznana kategoria",
                    v => v.TotalAmount
                );

            return transactionSummary;
        }


        public async Task<TransactionSummary> GetOverallTransactionSummaryAsync()
        {
            var transactionSummary = new TransactionSummary
            {
                TotalIncome = await _context.Transactions.Where(t => t.Amount > 0).SumAsync(t => t.Amount),
                TotalExpense = await _context.Transactions.Where(t => t.Amount < 0).SumAsync(t => t.Amount),
                TotalTransactionsCount = await _context.Transactions.CountAsync(),
                HighestTransactionAmount = await _context.Transactions.MaxAsync(t => t.Amount),
                LowestTransactionAmount = await _context.Transactions.MinAsync(t => t.Amount),
                MostRecentTransactionDate = await _context.Transactions.MaxAsync(t => t.Date)
            };

            var categoryTotals = await _context.Transactions
                .GroupBy(t => t.CategoryId)
                .Select(group => new { CategoryId = group.Key, TotalAmount = group.Sum(t => t.Amount) })
                .ToListAsync();

            var categoryNames = await _context.Categories
                .Where(c => categoryTotals.Select(ct => ct.CategoryId).Contains(c.Id))
                .ToDictionaryAsync(c => c.Id, c => c.Name);

            transactionSummary.TotalByCategory = categoryTotals
                .ToDictionary(
                    k => categoryNames.ContainsKey(k.CategoryId) ? categoryNames[k.CategoryId] : "Nieznana kategoria",
                    v => v.TotalAmount
                );

            return transactionSummary;
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
