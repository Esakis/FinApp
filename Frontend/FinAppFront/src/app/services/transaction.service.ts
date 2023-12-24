import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Transaction } from '../models/transaction.model';
import { TransactionSummary } from '../models/transactionSummary.model';
import { Category } from '../models/category.model';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private baseUrl = 'https://localhost:44367/Transactions'; 
  private categoriesSubject = new BehaviorSubject<Category[]>([]);
  public categories$ = this.categoriesSubject.asObservable();

  constructor(private http: HttpClient) { }

  loadCategories() {
    this.http.get<Category[]>(`${this.baseUrl}/getAllCategories`).subscribe(
      categories => this.categoriesSubject.next(categories),
      error => console.error('Error fetching categories', error)
    );
  }

  addTransaction(transaction: Transaction): Observable<Transaction> {
    return this.http.post<Transaction>(`${this.baseUrl}`, transaction);
  }

  getAllTransactions(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.baseUrl}`);
  }

  deleteTransaction(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }  

  getTransactionsSummary(): Observable<TransactionSummary> {
    return this.http.get<TransactionSummary>(`${this.baseUrl}/summary`);
  }

}
