import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserModule } from '@angular/platform-browser';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { CreateFamilyComponent } from './components/create-family/create-family.component';
import { InvitationComponent } from './components/invitation/invitation.component';
import { InvitationsComponent } from './components/invitations/invitations.component';
import { JoinFamilyComponent } from './components/join-family/join-family.component';
import { LoginComponent } from './components/login-register/login/login.component';
import { RegisterComponent } from './components/login-register/register/register.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { PantryItemAddComponent } from './components/pantry-item-add/pantry-item-add.component';
import { PantryItemComponent } from './components/pantry-item/pantry-item.component';
import { PantryComponent } from './components/pantry/pantry.component';
import { RecipeAddComponent } from './components/recipe-add/recipe-add.component';
import { RecipeEditComponent } from './components/recipe-edit/recipe-edit.component';
import { RecipeIngredientAddComponent } from './components/recipe-ingredient-add/recipe-ingredient-add.component';
import { RecipeIngredientEditComponent } from './components/recipe-ingredient-edit/recipe-ingredient-edit.component';
import { RecipeIngredientsComponent } from './components/recipe-ingredients/recipe-ingredients.component';
import { RecipeComponent } from './components/recipe/recipe.component';
import { RecipesComponent } from './components/recipes/recipes.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    PantryComponent,
    NavbarComponent,
    JoinFamilyComponent,
    CreateFamilyComponent,
    PantryItemComponent,
    PantryItemAddComponent,
    RecipesComponent,
    RecipeComponent,
    RecipeIngredientsComponent,
    RecipeAddComponent,
    RecipeIngredientAddComponent,
    InvitationsComponent,
    InvitationComponent,
    RecipeEditComponent,
    RecipeIngredientEditComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatMenuModule,
    MatToolbarModule,
    FormsModule,
    MatSnackBarModule,
    HttpClientModule,
    MatIconModule,
    MatBadgeModule,
    MatCardModule,
    MatChipsModule,
    MatSelectModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
