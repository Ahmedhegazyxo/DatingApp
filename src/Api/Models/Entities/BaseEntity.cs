namespace Api.Entities;
public class BaseEntity
{
    public int Id {get;set;}
    public DateTime CreatedDate {get;set;}
    public DateTime? LastModificationDate {get;set;}
}
public class BaseEntity<T>  where T : IEquatable<T>
{
    public T Id {get;set;} = default!;
    public DateTime CreatedDate {get;set;}
    public DateTime? LastModificationDate {get;set;}
}