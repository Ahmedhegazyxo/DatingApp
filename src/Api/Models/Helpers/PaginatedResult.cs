namespace Api.Helpers;
public class PaginatedResult<T> where T : class
{
    public int PageNumber {get;set;}
    public int PageSize {get;set;}
    public int TotalCount {get;set;}
    public List<T> Body {get;set;} = []; 
}