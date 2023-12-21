import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TransactionListComponent } from './components/transaction-list/transaction-list.component';
import { SummaryComponent } from './components/summary/summary.component';
import { TransactionFormComponent } from './components/transaction-form/transaction-form.component';

const routes: Routes = [
  { path: '', component: TransactionFormComponent },
  { path: 'transactions', component: TransactionListComponent },
  { path: 'summary', component: SummaryComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
