import { Component } from '@angular/core';
import { MatchesList } from "../../matches/matches-list/matches-list";

@Component({
  selector: 'app-matches-page',
  imports: [MatchesList],
  templateUrl: './matches-page.html',
  styleUrl: './matches-page.css',
})
export class MatchesPage {}
