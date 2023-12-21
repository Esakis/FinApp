import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Category } from 'src/app/models/category.model';
import { Transaction } from 'src/app/models/transaction.model';
import { TransactionService } from 'src/app/services/transaction.service';

@Component({
  selector: 'app-transaction-form',
  templateUrl: './transaction-form.component.html',
  styleUrls: ['./transaction-form.component.scss']
})
export class TransactionFormComponent implements OnInit {
  @Output() addTransaction = new EventEmitter<Transaction>();
  transactionForm: FormGroup;
  categories: Category[] = [];

  constructor(
    private fb: FormBuilder,
    private transactionService: TransactionService
  ) {
    this.transactionForm = this.fb.group({
      amount: [null, [Validators.required, Validators.min(0)]],
      date: [null, Validators.required],
      description: [null, Validators.required],
      categoryId: [null, Validators.required]
    });
  }

  ngOnInit(): void {
    this.transactionService.getAllCategories().subscribe(
      (data: Category[]) => {
        this.categories = data;
      },
      (error) => {
        console.error('Error fetching categories', error);
      }
    );
  }

  onSubmit(): void {
    if (this.transactionForm.valid) {
      const newTransaction: Transaction = this.transactionForm.value;
      this.addTransaction.emit(newTransaction);
      this.transactionForm.reset();
    }
  }
}
