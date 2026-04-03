import { Component, Inject, OnInit, OnDestroy } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { Category } from 'src/app/models/category.model';
import { Transaction } from 'src/app/models/transaction.model';
import { TransactionService } from 'src/app/services/transaction.service';

@Component({
  selector: 'app-edit-transaction-modal',
  templateUrl: './edit-transaction-modal.component.html',
  styleUrls: ['./edit-transaction-modal.component.scss']
})
export class EditTransactionModalComponent implements OnInit, OnDestroy {
  categories: Category[] = [];
  private categoriesSubscription: Subscription = new Subscription;

  constructor(
    private transactionService: TransactionService,
    private dialogRef: MatDialogRef<EditTransactionModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { transaction: Transaction }
  ) {}

  ngOnInit() {
    this.categoriesSubscription = this.transactionService.categories$.subscribe(
      (data) => {
        this.categories = data;
      }
    );
  }

  getCategoryName(categoryId: number): string {
    const category = this.categories.find(c => c.id === categoryId);
    return category ? category.name : 'Nieznana kategoria';
  }

  onCancel(): void {
    this.dialogRef.close(); 
  }

  ngOnDestroy(): void {
    if (this.categoriesSubscription) {
      this.categoriesSubscription.unsubscribe();
    }
  }
}
