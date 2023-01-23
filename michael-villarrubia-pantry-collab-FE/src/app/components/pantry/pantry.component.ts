import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription, take } from 'rxjs';
import { Pantry } from 'src/app/Models/Pantry';
import { UiService } from 'src/app/Services/ui.service';
import { showPage } from 'src/app/showPage';

@Component({
  selector: 'app-pantry',
  templateUrl: './pantry.component.html',
  styleUrls: ['./pantry.component.css'],
})
export class PantryComponent implements OnInit, OnDestroy {
  pantry = new Pantry(0, [], 0);

  $pantrySub = new Subscription();

  constructor(private uiService: UiService) {}

  ngOnInit(): void {
    this.$pantrySub = this.uiService.$pantry.subscribe(
      (pantry) => (this.pantry = pantry)
    );
  }

  ngOnDestroy(): void {
    this.$pantrySub.unsubscribe();
  }

  addItem(): void {
    this.uiService.$currentPage.next(showPage.pantryItemAdd);
  }
}
