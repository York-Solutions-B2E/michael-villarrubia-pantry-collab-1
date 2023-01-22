import { Component } from '@angular/core';
import { UiService } from 'src/app/Services/ui.service';

@Component({
  selector: 'app-join-family',
  templateUrl: './join-family.component.html',
  styleUrls: ['./join-family.component.css'],
})
export class JoinFamilyComponent {
  joinCode: string = '';
  password: string = '';

  constructor(private uiService: UiService) {}

  joinFamily(): void {
    this.uiService.joinFamily(this.joinCode, this.password);
  }
}
