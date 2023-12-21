namespace Store.Models
{
    public class TransactionSummary
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance => TotalIncome + TotalExpense; // Saldo

        public int TotalTransactionsCount { get; set; } // Łączna liczba transakcji
        public decimal AverageIncomePerTransaction => TotalTransactionsCount > 0 ? TotalIncome / TotalTransactionsCount : 0; // Średni dochód na transakcję
        public decimal AverageExpensePerTransaction => TotalTransactionsCount > 0 ? TotalExpense / TotalTransactionsCount : 0; // Średni wydatek na transakcję
        public decimal HighestTransactionAmount { get; set; } // Największa transakcja
        public decimal LowestTransactionAmount { get; set; } // Najmniejsza transakcja
        public DateTime MostRecentTransactionDate { get; set; } // Data najnowszej transakcji
        public Dictionary<string, decimal> TotalByCategory { get; set; } // Suma transakcji według kategorii
    }

}
