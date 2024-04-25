﻿using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.CreateVote;
public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
{
    public async Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(SozlukConstants.VoteExchangeName, SozlukConstants.DefaultExchangeType, SozlukConstants.CreateEntryVoteQueueName, new CreateEntryVoteEvent()
        {
            EntryId = request.EntryId,
            CreatedBy = request.CreatedBy,
            VoteType = request.VoteType
        });
        return await Task.FromResult(true);
    }
}
