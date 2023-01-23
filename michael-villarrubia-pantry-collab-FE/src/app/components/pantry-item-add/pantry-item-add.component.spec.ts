import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PantryItemAddComponent } from './pantry-item-add.component';

describe('PantryItemAddComponent', () => {
  let component: PantryItemAddComponent;
  let fixture: ComponentFixture<PantryItemAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PantryItemAddComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PantryItemAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
