import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

import { TransactionService } from 'src/app/services/transaction.service';
import { UserService } from 'src/app/services/user.service';
import { Transaction } from 'src/app/models/transaction.model';
import { Category } from 'src/app/models/category.model';

@Component({
  selector: 'app-transaction-form',
  templateUrl: './transaction-form.component.html',
  styleUrls: ['./transaction-form.component.scss']
})
export class TransactionFormComponent implements OnInit, OnDestroy {
  transactionForm!: FormGroup;
  categories: Category[] = [];
  private categoriesSubscription: Subscription | undefined;

  constructor(
    private fb: FormBuilder,
    private transactionService: TransactionService,
    private userService: UserService,
    private snackBar: MatSnackBar
  ) {
    this.createForm();
  }

  ngOnInit(): void {
    this.categoriesSubscription = this.transactionService.categories$.subscribe(
      (data: Category[]) => {
        this.categories = data;
      }
    );
  }

  createForm() {
    const today = new Date();
    const currentDate = today.toISOString().substring(0, 10);

    this.transactionForm = this.fb.group({
      amount: [null, [Validators.required, Validators.min(0)]],
      date: [currentDate, Validators.required],
      description: [null, Validators.required],
      categoryId: [null, Validators.required],
      userId: [1, Validators.required]
    });
  }

  onSubmit(): void {
    if (this.transactionForm.valid) {
      const newTransaction: Transaction = this.transactionForm.value;
      this.transactionService.addTransaction(newTransaction).subscribe(
        () => {
          this.snackBar.open('Transakcja dodana pomyślnie', 'Zamknij', {
            duration: 3000
          });
          this.createForm();
          this.userService.refreshUser(); 
        },
        (error) => {
          this.snackBar.open('Wystąpił błąd podczas dodawania transakcji', 'Zamknij', {
            duration: 3000
          });
          console.error('Error adding transaction', error);
        }
      );
    }
  }

  ngOnDestroy(): void {
    if (this.categoriesSubscription) {
      this.categoriesSubscription.unsubscribe();
    }
  }
}
