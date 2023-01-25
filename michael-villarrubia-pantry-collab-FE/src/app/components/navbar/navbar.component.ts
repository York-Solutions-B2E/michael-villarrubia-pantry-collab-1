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
  familyCode: string = '';
  userId: number = 0;

  $familySub = new Subscription();
  $userIdSub = new Subscription();

  constructor(public uiService: UiService) {}

  ngOnInit(): void {
    this.$familySub = this.uiService.$family.subscribe((family) => {
      this.familyId = family.id;
      this.familyCode = family.code;
      this.familyName = family.name
        .charAt(0)
        .toUpperCase()
        .concat(family.name.slice(1, family.name.length));
    });

    this.$userIdSub = this.uiService.$userId.subscribe(
      (userId) => (this.userId = userId)
    );
  }

  ngOnDestroy(): void {
    this.$familySub.unsubscribe();
  }

  logout(): void {
    this.uiService.logout();
  }

  showBackButton(): boolean {
    if (
      this.currPage === 'login' ||
      this.currPage === 'register' ||
      this.currPage === 'pantry' ||
      this.currPage === 'joinFamily' ||
      this.currPage === 'createFamily'
    ) {
      return false;
    }
    return true;
  }

  hasFamily(): boolean {
    return this.familyId ? true : false;
  }

  isLoggedIn(): boolean {
    return this.userId != 0 ? true : false;
  }

  backToPantry(): void {
    this.uiService.$currentPage.next(showPage.pantry);
    this.uiService.getPantry(this.familyId);
  }

  goToRecipes(): void {
    this.uiService.$currentPage.next(showPage.recipes);
    this.uiService.getIngredients(this.familyId);
    this.uiService.getRecipes(this.familyId);
  }

  goToInvitations(): void {
    this.uiService.$currentPage.next(showPage.invitations);
    this.uiService.getInvitations(this.familyId);
  }
}
