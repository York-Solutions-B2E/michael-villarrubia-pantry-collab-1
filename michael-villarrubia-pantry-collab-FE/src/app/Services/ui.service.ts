import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BehaviorSubject, Observable, ReplaySubject, take } from 'rxjs';
import { Family } from '../Models/Family';
import { Ingredient } from '../Models/Ingredient';
import { Invitation } from '../Models/Invitation';
import { Pantry } from '../Models/Pantry';
import { PantryItem } from '../Models/PantryItem';
import { Recipe } from '../Models/Recipe';
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
  $recipes = new BehaviorSubject<Recipe[]>([]);
  $createdRecipe = new BehaviorSubject<Recipe>(
    new Recipe(0, '', '', '', '', [])
  );
  $invitations = new BehaviorSubject<Invitation[]>([]);

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
          this.$userId.next(newUser.id);
          this.$username.next(newUser.username);
          this.$familyId.next(newUser.familyId);
          this.$currentPage.next(showPage.pantry);
          localStorage.setItem('username', newUser.username);
          localStorage.setItem('password', newUser.password);
          if (user.familyId) {
            this.getPantry(user.familyId);
            this.getFamily(user.familyId);
            this.getRecipes(user.familyId);
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
            this.getRecipes(user.familyId);
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
          this.getRecipes(familyId);
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

  joinFamily(code: string): void {
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

  createFamily(name: string): void {
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

  getRecipes(familyId: number): void {
    this.http
      .get<Recipe[]>(
        `https://localhost:7201/api/Recipes/getRecipes?familyId=${familyId}`
      )
      .pipe(take(1))
      .subscribe({
        next: (recipes) => {
          this.$recipes.next(recipes);
        },
        error: (err) => {
          this.openSnackBar(err.error);
        },
      });
  }

  addRecipe(recipe: Recipe): void {
    this.http
      .post<Recipe>(
        `https://localhost:7201/api/Recipes/add?familyId=${this.$familyId.value}`,
        recipe
      )
      .pipe(take(1))
      .subscribe({
        next: (recipe) => {
          if (this.$familyId.value) {
            this.getRecipes(this.$familyId.value);
          }
          this.$createdRecipe.next(recipe);
        },
        error: (err) => {
          this.openSnackBar(err.error);
        },
      });
  }

  addIngredient(quantity: number, ingredient: Ingredient, recipeId: number) {
    this.http
      .post<Ingredient>(
        `https://localhost:7201/api/Ingredients/add?quantity=${quantity}&recipeId=${recipeId}`,
        ingredient
      )
      .pipe(take(1))
      .subscribe({
        next: (ingredient) => {
          let updatedRecipe = this.$createdRecipe.value;
          updatedRecipe.ingredients.push(ingredient);
          this.$createdRecipe.next(updatedRecipe);
        },
        error: (err) => {
          this.openSnackBar(err.error);
        },
      });
  }

  sendInvite(senderFamId: number, receiverCode: string) {
    this.http
      .post<Invitation>(
        `https://localhost:7201/api/Invitations/send?senderFamId=${senderFamId}&receiverFamCode=${receiverCode}`,
        {}
      )
      .pipe(take(1))
      .subscribe({
        next: (invitation) => {
          this.getInvitations(invitation.senderFamilyId);
        },
      });
  }

  respondToInvite(invitationId: number, response: boolean) {
    this.http
      .post<Invitation>(
        `https://localhost:7201/api/Invitations/respond?invitationId=${invitationId}&response=${response}`,
        {}
      )
      .pipe(take(1))
      .subscribe({
        next: (invitation) => {
          this.getInvitations(invitation.receiverFamilyId);
        },
      });
  }

  getInvitations(familyId: number) {
    this.http
      .get<Invitation[]>(
        `https://localhost:7201/api/Invitations?familyId=${familyId}`
      )
      .pipe(take(1))
      .subscribe({
        next: (invitations) => {
          this.$invitations.next(invitations);
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
