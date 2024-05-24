namespace BlazorSozluk.Common.Models.Queries;
public class UserDetailViewModel
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string EmailAdress { get; set; }
    public bool EmailConfirmed { get; set; }
}
