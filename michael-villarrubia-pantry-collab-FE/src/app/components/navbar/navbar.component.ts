import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { UiService } from 'src/app/Services/ui.service';
import { showPage } from 'src/app/showPage';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit, OnDestroy {
  @Input() currPage: string = '';
  familyName: string = '';
  familyId: number = 0;

  $familySub = new Subscription();

  constructor(public uiService: UiService) {}

  ngOnInit(): void {
    this.$familySub = this.uiService.$family.subscribe((family) => {
      this.familyId = family.id;
      this.familyName = family.name
        .charAt(0)
        .toUpperCase()
        .concat(family.name.slice(1, family.name.length));
    });
  }

  ngOnDestroy(): void {
    this.$familySub.unsubscribe();
  }

  logout(): void {
    this.uiService.logout();
  }

  showBackButton(): boolean {
    if (
      this.currPage.toLowerCase().includes('add') ||
      this.currPage.toLowerCase().includes('update')
    ) {
      return true;
    }
    return false;
  }

  backToPantry(): void {
    this.uiService.$currentPage.next(showPage.pantry);
    this.uiService.getPantry(this.familyId);
  }

  goToRecipes(): void {
    this.uiService.$currentPage.next(showPage.recipes);
    this.uiService.getRecipes(this.familyId);
  }
}
