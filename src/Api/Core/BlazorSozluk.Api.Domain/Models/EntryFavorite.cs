namespace BlazorSozluk.Api.Domain.Models;

public class EntryFavorite : BaseEntity
{
    public Guid EntryId { get; set; }
    public string CreatedById { get; set; }
    public virtual Entry Entry { get; set; }
    public virtual User CreatedUser { get; set; }
}
