import { Component, Input, OnInit } from '@angular/core';
import { Invitation } from 'src/app/Models/Invitation';
import { UiService } from 'src/app/Services/ui.service';

@Component({
  selector: 'app-invitation',
  templateUrl: './invitation.component.html',
  styleUrls: ['./invitation.component.css'],
})
export class InvitationComponent implements OnInit {
  @Input() invitation: Invitation = new Invitation(0, 0, 0, null);
  @Input() familyId: number = 0;

  constructor(public uiService: UiService) {}

  ngOnInit(): void {}

  respond(response: boolean): void {
    this.uiService.respondToInvite(this.invitation.id, response);
  }
}
