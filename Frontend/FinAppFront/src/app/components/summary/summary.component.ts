import { Component, OnInit } from '@angular/core';
import { TransactionSummary } from 'src/app/models/transactionSummary.model';
import { TransactionService } from 'src/app/services/transaction.service';


@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.scss']
})
export class SummaryComponent implements OnInit {
  summary: TransactionSummary | null = null;

  constructor(private transactionService: TransactionService) {}

  ngOnInit() {
    this.transactionService.getTransactionsSummary().subscribe(
      (data) => {
        this.summary = data;
      }
    );
  }
}
