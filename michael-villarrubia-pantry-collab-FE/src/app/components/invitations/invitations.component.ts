import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Invitation } from 'src/app/Models/Invitation';
import { UiService } from 'src/app/Services/ui.service';

@Component({
  selector: 'app-invitations',
  templateUrl: './invitations.component.html',
  styleUrls: ['./invitations.component.css'],
})
export class InvitationsComponent implements OnInit {
  receiverCode: string = '';
  familyId: number = 0;
  invitations: Invitation[] = [];
  incomingInvites: Invitation[] = [];
  $invitationsSub = new Subscription();

  constructor(public uiService: UiService) {}

  ngOnInit(): void {
    this.$invitationsSub = this.uiService.$invitations.subscribe(
      (invitations) => {
        this.invitations = invitations;
        this.incomingInvites = invitations.filter(
          (i) => i.receiverFamilyId === this.familyId && i.accepted === null
        );
      }
    );
    if (this.uiService.$familyId.value)
      this.familyId = this.uiService.$familyId.value;
  }

  sendInvite(): void {
    this.uiService.sendInvite(this.familyId, this.receiverCode);
  }
}
