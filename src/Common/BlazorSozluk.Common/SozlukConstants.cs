namespace BlazorSozluk.Common;
public class SozlukConstants
{
    public const string RabbitMqHost = "localhost";
    public const string DefaultExchangeType = "direct";

    //User Exchange
    public const string UserExchangeName = "UserExchange";

    public const string UserEmailChangedQueueName = "UserEmailChangedQueue";

    //Fav Exchange 
    public const string FavExchangeName = "FavExchange";

    public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueue";
    public const string CreateEntryFavQueueName = "CreateEntryFavQueue";
    public const string DeleteEntryFavQueueName = "DeleteEntryFavQueue";
    public const string DeleteEntryCommentFavQueueName = "DeleteEntryCommentFavQueue";

    //Vote Exchange
    public const string VoteExchangeName = "VoteExchange";

    public const string CreateEntryVoteQueueName = "CreateEntryVoteQueue";
    public const string DeleteEntryVoteQueueName = "DeleteEntryVoteQueue";
    public const string CreateEntryCommentVoteQueueName = "CreateEntryCommentVoteQueue";
    public const string DeleteEntryCommentVoteQueueName = "DeleteEntryCommentVoteQueue";
}
