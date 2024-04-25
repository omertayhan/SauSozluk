using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteVote;
public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(SozlukConstants.VoteExchangeName, SozlukConstants.DefaultExchangeType, SozlukConstants.DeleteEntryVoteQueueName, new DeleteEntryVoteEvent()
        {
            EntryId = request.EntryId,
            CreatedBy = request.UserId
        });
        return await Task.FromResult(true);
    }
}
