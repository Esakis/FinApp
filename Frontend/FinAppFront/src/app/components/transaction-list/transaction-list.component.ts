import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Category } from 'src/app/models/category.model';
import { Transaction } from 'src/app/models/transaction.model';
import { TransactionService } from 'src/app/services/transaction.service';
import { MatDialog } from '@angular/material/dialog';
import { EditTransactionModalComponent } from 'src/app/shared/edit-transaction-modal/edit-transaction-modal.component';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.scss']
})
export class TransactionListComponent implements OnInit, OnDestroy {
  transactions: Transaction[] = [];
  categories: Category[] = [];
  isSortAsc: boolean = true; 
  private categoriesSubscription: Subscription = new Subscription;

  constructor(private transactionService: TransactionService,
    private dialog: MatDialog) {}

  ngOnInit() {
    this.loadTransactions();
    this.categoriesSubscription = this.transactionService.categories$.subscribe(
      (data) => {
        this.categories = data;
      }
    );
  }

  loadTransactions() {
    this.transactionService.getAllTransactions().subscribe(
      (data) => {
        this.transactions = data;
      },
      (error) => {
        console.error('Error fetching transactions', error);
      }
    );
  }

  deleteTransaction(id: number): void {
    this.transactionService.deleteTransaction(id).subscribe(() => {
      this.transactions = this.transactions.filter(transaction => transaction.id !== id);
    }, error => {
      console.error('Error deleting transaction', error);
    });
  }

  editTransaction(transaction: Transaction): void {
    this.dialog.open(EditTransactionModalComponent, { data: { transaction } });
  }

  getCategoryName(categoryId: number): string {
    const category = this.categories.find(c => c.id === categoryId);
    return category ? category.name : 'Nieznana kategoria';
  }

  sortByCategory() {
    this.transactions.sort((a, b) => {
      const categoryA = this.categories.find(c => c.id === a.categoryId);
      const categoryB = this.categories.find(c => c.id === b.categoryId);
  
      const nameA = categoryA ? categoryA.name : "";
      const nameB = categoryB ? categoryB.name : "";
  
      return this.isSortAsc ? nameA.localeCompare(nameB) : nameB.localeCompare(nameA);
    });
  
    this.isSortAsc = !this.isSortAsc;
  }

  ngOnDestroy(): void {
    if (this.categoriesSubscription) {
      this.categoriesSubscription.unsubscribe();
    }
  }
}
