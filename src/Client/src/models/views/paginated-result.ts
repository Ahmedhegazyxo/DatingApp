export class PaginatedResult<T> {
    public body: Array<T> = [];
    public totalCount: number = 0;
    public pageNumber: number = 1;
    public pageSize: number = 50;
}