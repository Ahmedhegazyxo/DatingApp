namespace Api.Entities;
public class LookupType : BaseEntity
{
    public virtual List<Lookup> Lookups {get;private set;} = [];
    public string Name {get;set;} = string.Empty;
}