import { Injectable } from '@angular/core';
import { ChatMessageDto } from '../models/views/chat-message-view';
import { PaginatedResult } from '../models/views/paginated-result';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginationFilter } from '../models/queries/paginationFilter';
import { MemberModel } from '../models/views/member-model';
import { Observable } from 'rxjs';
import { SendMessageCommand } from '../models/commands/send-message-command';

@Injectable({
  providedIn: 'root',
})
export class ChatMessagesService {
  private URI: string = 'https://localhost:7111/api/chat/';
  constructor(private httpClient: HttpClient) {
  }
  getMatchMessageThread(matchId: string, paginationFilter: PaginationFilter): Observable<PaginatedResult<ChatMessageDto>> {
    let params = new HttpParams();
    params = params.append('pageNumber', paginationFilter.pageNumber);
    params = params.append('pageSize', paginationFilter.pageSize);
    return this.httpClient.get<PaginatedResult<ChatMessageDto>>(this.URI + matchId + '/messages', { params });
  }
  sendMessage(matchId: string, messageBody: string) {
    return this.httpClient.post<any>(this.URI, new SendMessageCommand(matchId, messageBody));
  }
}
