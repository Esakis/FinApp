import { Category } from "./category.model";

export interface Transaction {
    id?: number; // ID transakcji (opcjonalne, jeśli jest to nowa transakcja)
    amount: number; // Kwota transakcji
    date: Date; // Data transakcji
    description: string; // Opis transakcji
    categoryId: number; // ID kategorii transakcji
    category?: Category; // Obiekt kategorii (opcjonalne)
  }