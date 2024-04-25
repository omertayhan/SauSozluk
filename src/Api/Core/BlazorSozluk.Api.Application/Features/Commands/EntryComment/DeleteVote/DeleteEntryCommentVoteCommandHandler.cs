using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infrastructure;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteVote;
public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(SozlukConstants.FavExchangeName, SozlukConstants.DefaultExchangeType, SozlukConstants.DeleteEntryCommentVoteQueueName, new DeleteEntryCommentVoteEvent()
        {
            EntryCommentId = request.EntryCommentId,
            CreatedBy = request.UserId
        });

        return await Task.FromResult(true);
    }
}
