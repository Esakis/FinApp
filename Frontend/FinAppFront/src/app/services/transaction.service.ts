import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Transaction } from '../models/transaction.model';
import { TransactionSummary } from '../models/transactionSummary.model';
import { Category } from '../models/category.model';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private baseUrl = 'https://localhost:44367/Transactions'; 

  constructor(private http: HttpClient) { }

  addTransaction(transaction: Transaction): Observable<Transaction> {
    return this.http.post<Transaction>(`${this.baseUrl}`, transaction);
  }

  // Pobieranie historii transakcji
  getAllTransactions(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.baseUrl}`);
  }

  deleteTransaction(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  getTransactionsSummary(): Observable<TransactionSummary> {
    return this.http.get<TransactionSummary>(`${this.baseUrl}/summary`);
  }
  getAllCategories(): Observable<Category[]> {
    const url = `${this.baseUrl}/getAllCategories`;
    return this.http.get<Category[]>(url);
  }
}
