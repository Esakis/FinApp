export interface TransactionSummary {
    totalIncome: number; // Łączny dochód
    totalExpense: number; // Łączny wydatek
    balance: number; // Saldo
    totalTransactionsCount: number; // Łączna liczba transakcji
    averageIncomePerTransaction: number; // Średni dochód na transakcję
    averageExpensePerTransaction: number; // Średni wydatek na transakcję
    highestTransactionAmount: number; // Największa transakcja
    lowestTransactionAmount: number; // Najmniejsza transakcja
    mostRecentTransactionDate: Date; // Data najnowszej transakcji
    totalByCategory: { [key: string]: number }; // Suma transakcji według kategorii (mapa)
  }
  