import { Component } from '@angular/core';
import { User } from 'src/app/Models/User';
import { UiService } from 'src/app/Services/ui.service';
import { showPage } from 'src/app/showPage';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  user: User = new User(0, '', '', null);

  constructor(private uiService: UiService) {}

  login(): void {
    this.uiService.login(this.user);
  }

  goToRegister(): void {
    this.uiService.$currentPage.next(showPage.register);
  }
}
