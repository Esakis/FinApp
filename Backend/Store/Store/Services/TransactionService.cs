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
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == transaction.CategoryId);
            var userExists = await _context.Users.AnyAsync(u => u.Id == transaction.UserId);

            if (!categoryExists || !userExists)
            {
                throw new InvalidOperationException("Kategoria lub użytkownik nie istnieje");
            }

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransactionModel>> GetAllTransactionsAsync()
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .Include(t => t.User)
                .ToListAsync();
        }

        public async Task<TransactionModel> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
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

            return new TransactionSummary
            {
                TotalIncome = await userTransactions.Where(t => t.Amount > 0).SumAsync(t => t.Amount),
                TotalExpense = await userTransactions.Where(t => t.Amount < 0).SumAsync(t => t.Amount),
                TotalTransactionsCount = await userTransactions.CountAsync(),
                HighestTransactionAmount = await userTransactions.MaxAsync(t => t.Amount),
                LowestTransactionAmount = await userTransactions.MinAsync(t => t.Amount),
                MostRecentTransactionDate = await userTransactions.MaxAsync(t => t.Date),
                TotalByCategory = await userTransactions
                                    .GroupBy(t => t.Category.Name)
                                    .Select(group => new { Category = group.Key, TotalAmount = group.Sum(t => t.Amount) })
                                    .ToDictionaryAsync(k => k.Category, v => v.TotalAmount)
            };
        }

        public async Task<TransactionSummary> GetOverallTransactionSummaryAsync()
        {
            return new TransactionSummary
            {
                TotalIncome = await _context.Transactions.Where(t => t.Amount > 0).SumAsync(t => t.Amount),
                TotalExpense = await _context.Transactions.Where(t => t.Amount < 0).SumAsync(t => t.Amount),
                TotalTransactionsCount = await _context.Transactions.CountAsync(),
                HighestTransactionAmount = await _context.Transactions.MaxAsync(t => t.Amount),
                LowestTransactionAmount = await _context.Transactions.MinAsync(t => t.Amount),
                MostRecentTransactionDate = await _context.Transactions.MaxAsync(t => t.Date),
                TotalByCategory = await _context.Transactions
                                    .GroupBy(t => t.Category.Name)
                                    .Select(group => new { Category = group.Key, TotalAmount = group.Sum(t => t.Amount) })
                                    .ToDictionaryAsync(k => k.Category, v => v.TotalAmount)
            };
        }

        public async Task<IEnumerable<TransactionModel>> GetTransactionsByCategoryAsync(int categoryId)
        {
            return await _context.Transactions
                        .Where(t => t.CategoryId == categoryId)
                        .Include(t => t.Category)
                        .Include(t => t.User)
                        .ToListAsync();
        }

    }
}
