import { Component, OnDestroy } from '@angular/core';
import { UiService } from './Services/ui.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnDestroy {
  currPage: string = '';

  constructor(private uiService: UiService) {
    this.uiService.$currentPage.subscribe((page) => {
      this.currPage = page;
    });
  }

  ngOnDestroy(): void {
    this.uiService.$currentPage.unsubscribe();
  }
}
