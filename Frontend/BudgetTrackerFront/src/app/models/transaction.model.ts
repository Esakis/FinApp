export interface Transaction {
    id: number; 
    amount: number; 
    date: Date; 
    description: string; 
    categoryId: number;
    userId: number;
  }