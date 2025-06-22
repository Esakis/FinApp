import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UserModel } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userSubject: BehaviorSubject<UserModel | null> = new BehaviorSubject<UserModel | null>(null);
  public user$: Observable<UserModel | null> = this.userSubject.asObservable();
  private baseUrl = 'https://localhost:7119';

  constructor(private http: HttpClient) {}

  loadUser(id: number): void {
    this.http.get<UserModel>(`${this.baseUrl}/Users/${id}`).subscribe(
      user => this.userSubject.next(user),
      error => console.error('Error fetching user data', error)
    );
  }

  refreshUser(): void {
    this.loadUser(1);
  }
}
