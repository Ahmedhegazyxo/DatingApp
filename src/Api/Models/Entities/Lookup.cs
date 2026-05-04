using Microsoft.AspNetCore.SignalR;

namespace Api.Entities;
public class Lookup : BaseEntity
{
    public LookupType? LookupType {get;private set;}
    public int LookupTypeId {get;set;}
    public string Name {get;set;} = string.Empty;
}