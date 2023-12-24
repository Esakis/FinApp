import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/models/category.model';
import { Transaction } from 'src/app/models/transaction.model';
import { TransactionService } from 'src/app/services/transaction.service';


@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.scss']
})
export class TransactionListComponent implements OnInit {
  transactions: Transaction[] = [];
  categories: Category[] = [];
  isSortAsc: boolean = true; 
  

  constructor(private transactionService: TransactionService) {}

  ngOnInit() {
    this.loadTransactions();
    this.loadCategories();
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

  loadCategories() {
    this.transactionService.getAllCategories().subscribe(
      (data) => {
        this.categories = data;
      },
      (error) => {
        console.error('Error fetching categories', error);
      }
    );
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
  
  
}