import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Chat } from "../../../chat/chat/chat";

@Component({
  selector: 'app-match-messages-page',
  imports: [Chat],
  templateUrl: './match-messages-page.html',
  styleUrl: './match-messages-page.css',
})
export class MatchMessagesPage  implements OnInit {
  constructor(private activatedRoute: ActivatedRoute) {

  }
  protected matchId: string | null = null;
  ngOnInit() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    this.matchId = id;
  }
}
