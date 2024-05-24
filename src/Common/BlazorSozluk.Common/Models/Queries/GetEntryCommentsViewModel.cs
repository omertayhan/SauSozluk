using BlazorSozluk.Common.Models.Queries.BaseFooter;

namespace BlazorSozluk.Common.Models.Queries;
public class GetEntryCommentsViewModel : BaseFooterRateFavoritedViewModel
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
}
