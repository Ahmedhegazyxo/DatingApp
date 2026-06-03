import { Component } from '@angular/core';
import { MatchesService } from '../../../services/matches-service';
import { PaginationFilter } from '../../../models/queries/paginationFilter';
import { PaginatedList } from "../../general/paginated-list/paginated-list";
import { Observable } from 'rxjs/internal/Observable';
import { PaginatedResult } from '../../../models/views/paginated-result';
import { MemberModel } from '../../../models/views/member-model';
import { MemberCard } from "../../members/member-card/member-card";

@Component({
  selector: 'app-matches-list',
  imports: [PaginatedList, MemberCard],
  templateUrl: './matches-list.html',
  styleUrl: './matches-list.css',
})
export class MatchesList {
  constructor(private matchesService: MatchesService) { }
   protected getMatches = (filter: PaginationFilter): Observable<PaginatedResult<MemberModel>> => {
      return this.matchesService.getMatches(filter);
    }
}
