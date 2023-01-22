import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { Pantry } from 'src/app/Models/Pantry';
import { UiService } from 'src/app/Services/ui.service';

@Component({
  selector: 'app-pantry',
  templateUrl: './pantry.component.html',
  styleUrls: ['./pantry.component.css'],
})
export class PantryComponent implements OnInit {
  pantry = new Pantry(0, [], 0);

  constructor(private uiService: UiService) {
    uiService.$pantry.subscribe((pantry) => (this.pantry = pantry));
  }

  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.uiService.$pantry.unsubscribe();
  }
}
