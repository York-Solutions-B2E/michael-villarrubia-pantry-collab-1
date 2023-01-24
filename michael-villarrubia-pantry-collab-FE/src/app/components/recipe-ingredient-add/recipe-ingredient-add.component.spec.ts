import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipeIngredientAddComponent } from './recipe-ingredient-add.component';

describe('RecipeIngredientAddComponent', () => {
  let component: RecipeIngredientAddComponent;
  let fixture: ComponentFixture<RecipeIngredientAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecipeIngredientAddComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecipeIngredientAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
