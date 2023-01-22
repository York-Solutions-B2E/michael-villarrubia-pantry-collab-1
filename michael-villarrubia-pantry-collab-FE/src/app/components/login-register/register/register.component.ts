import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Models/User';
import { UiService } from 'src/app/Services/ui.service';
import { showPage } from 'src/app/showPage';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  user: User = new User(0, '', '', null);
  confirmPass: string = '';

  constructor(private uiService: UiService) {}

  ngOnInit(): void {
    let username = localStorage.getItem('username');
    let password = localStorage.getItem('password');
    if (username && password) {
      this.user.username = username;
      this.user.password = password;
      this.uiService.login(this.user);
    }
  }

  register(): void {
    if (this.confirmPass != this.user.password) {
      this.uiService.openSnackBar("Passwords don't match");
      return;
    }
    this.uiService.register(this.user);
  }

  goToLogin(): void {
    this.uiService.$currentPage.next(showPage.login);
  }
}
