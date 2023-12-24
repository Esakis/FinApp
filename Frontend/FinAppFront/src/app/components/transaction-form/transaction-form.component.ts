import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Category } from 'src/app/models/category.model';
import { TransactionService } from 'src/app/services/transaction.service';
import { Subscription } from 'rxjs';
import { Transaction } from 'src/app/models/transaction.model';

@Component({
  selector: 'app-transaction-form',
  templateUrl: './transaction-form.component.html',
  styleUrls: ['./transaction-form.component.scss']
})
export class TransactionFormComponent implements OnInit, OnDestroy {
  transactionForm: FormGroup;
  categories: Category[] = [];
  private categoriesSubscription: Subscription | undefined;

  constructor(
    private fb: FormBuilder,
    private transactionService: TransactionService
  ) {
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

  ngOnInit(): void {
    this.categoriesSubscription = this.transactionService.categories$.subscribe(
      (data: Category[]) => {
        this.categories = data;
        console.log(data)
      }
    );
  }

  onSubmit(): void {
    if (this.transactionForm.valid) {
      const newTransaction: Transaction = this.transactionForm.value;
      this.transactionService.addTransaction(newTransaction).subscribe(
        (response) => {
          console.log('Transakcja dodana pomyślnie', response);
          this.transactionForm.reset();
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
