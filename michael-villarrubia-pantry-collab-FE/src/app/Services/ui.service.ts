import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BehaviorSubject, Observable, ReplaySubject, take } from 'rxjs';
import { Family } from '../Models/Family';
import { Pantry } from '../Models/Pantry';
import { PantryItem } from '../Models/PantryItem';
import { User } from '../Models/User';
import { showPage } from '../showPage';

@Injectable({
  providedIn: 'root',
})
export class UiService {
  $userId = new BehaviorSubject<number>(0);
  $username = new BehaviorSubject<string>('');
  $familyId = new BehaviorSubject<number | null>(null);

  $currentPage = new BehaviorSubject<showPage>(showPage.register);

  $pantry = new BehaviorSubject<Pantry>(new Pantry(0, [], 0));
  $family = new BehaviorSubject<Family>(new Family(0, '', '', [], [], []));

  constructor(private http: HttpClient, public _snackbar: MatSnackBar) {}

  openSnackBar(message: string) {
    this._snackbar.open(message, '', { duration: 2000 });
  }

  register(user: User): void {
    this.http
      .post<User>('https://localhost:7201/api/Users/register', user)
      .pipe(take(1))
      .subscribe({
        next: (newUser) => {
          this.$userId.next(user.id);
          this.$username.next(newUser.username);
          this.$familyId.next(newUser.familyId);
          this.$currentPage.next(showPage.pantry);
          localStorage.setItem('username', newUser.username);
          localStorage.setItem('password', newUser.password);
          if (user.familyId) {
            this.getPantry(user.familyId);
            this.getFamily(user.familyId);
          } else {
            this.$currentPage.next(showPage.joinFamily);
          }
        },
        error: (err) => {
          this.openSnackBar(err.error);
        },
      });
  }

  login(user: User): void {
    this.http
      .post<User>('https://localhost:7201/api/Users/login', user)
      .pipe(take(1))
      .subscribe({
        next: (user) => {
          this.$userId.next(user.id);
          this.$username.next(user.username);
          this.$familyId.next(user.familyId);
          this.$currentPage.next(showPage.pantry);
          localStorage.setItem('username', user.username);
          localStorage.setItem('password', user.password);
          if (user.familyId) {
            this.getPantry(user.familyId);
            this.getFamily(user.familyId);
          } else {
            this.$currentPage.next(showPage.joinFamily);
          }
        },
        error: (err) => {
          this.openSnackBar(err.error);
        },
      });
  }

  getPantry(familyId: number): void {
    this.http
      .get<Pantry>(`https://localhost:7201/api/Pantries?familyId=${familyId}`)
      .pipe(take(1))
      .subscribe({
        next: (pantry) => {
          this.$pantry.next(pantry);
        },
        error: (err) => {
          this.openSnackBar(err.error);
        },
      });
  }

  addItemToPantry(item: PantryItem): void {
    this.http
      .post<Pantry>(
        `https://localhost:7201/api/Pantries/addItem?familyId=${this.$familyId.value}`,
        item
      )
      .pipe(take(1))
      .subscribe({
        next: (pantry) => {
          this.$pantry.next(pantry);
        },
        error: (err) => {
          this.openSnackBar(err.error);
        },
      });
  }

  getFamily(familyId: number): void {
    this.http
      .get<Family>(`https://localhost:7201/api/Families/?familyId=${familyId}`)
      .pipe(take(1))
      .subscribe({
        next: (family) => {
          this.$family.next(family);
        },
      });
  }

  joinFamily(code: string) {
    this.http
      .patch<Family>(
        `https://localhost:7201/api/Families/join?code=${code}&userId=${this.$userId.value}`,
        {}
      )
      .pipe(take(1))
      .subscribe({
        next: (family) => {
          this.$family.next(family);
          this.$familyId.next(family.id);
          this.getPantry(family.id);
          this.$currentPage.next(showPage.pantry);
        },
        error: (err) => {
          this.openSnackBar(err.error);
        },
      });
  }

  createFamily(name: string) {
    this.http
      .post<Family>(
        `https://localhost:7201/api/Families/create?userId=${this.$userId.value}`,
        { name: name }
      )
      .pipe(take(1))
      .subscribe({
        next: (family) => {
          this.$family.next(family);
          this.$familyId.next(family.id);
          this.getPantry(family.id);
          this.$currentPage.next(showPage.pantry);
        },
        error: (err) => {
          this.openSnackBar(err.error);
        },
      });
  }

  logout(): void {
    this.$familyId.next(null);
    this.$username.next('');
    localStorage.clear();
    this.$currentPage.next(showPage.login);
  }
}
