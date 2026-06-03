export class SendMessageCommand {
    matchId: string = '';
    messageBody: string = '';
    constructor(matchId: string, messageBody: string) {
        this.matchId = matchId;
        this.messageBody = messageBody;
    }
}