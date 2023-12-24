import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { Category } from 'src/app/models/category.model';
import { Transaction } from 'src/app/models/transaction.model';
import { TransactionService } from 'src/app/services/transaction.service';

@Component({
  selector: 'app-edit-transaction-modal',
  templateUrl: './edit-transaction-modal.component.html',
  styleUrls: ['./edit-transaction-modal.component.scss']
})
export class EditTransactionModalComponent implements OnInit {
onCancel() {
throw new Error('Method not implemented.');
}
  editForm: FormGroup;
  categories: Category[] = [];
  private categoriesSubscription: Subscription = new Subscription;

  constructor(
    private transactionService: TransactionService,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: { transaction: Transaction }
  ) {
    this.editForm = this.fb.group({
      amount: [data.transaction.amount],
      date: [data.transaction.date],
      description: [data.transaction.description],
      categoryId: [data.transaction.categoryId]
    });
  }

  ngOnInit() {
    this.categoriesSubscription = this.transactionService.categories$.subscribe(
      (data) => {
        this.categories = data;
      }
    );
  }

  onSubmit(): void {
  }

  ngOnDestroy(): void {
    if (this.categoriesSubscription) {
      this.categoriesSubscription.unsubscribe();
    }
  }
}
