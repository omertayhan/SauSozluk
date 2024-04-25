﻿using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteFav;
public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(SozlukConstants.FavExchangeName, SozlukConstants.DefaultExchangeType, SozlukConstants.DeleteEntryFavQueueName, new DeleteEntryFavEvent()
        {
            EntryId = request.EntryId,
            CreatedBy = request.UserId
        });
        return await Task.FromResult(true);
    }
}
