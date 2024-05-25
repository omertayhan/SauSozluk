﻿namespace BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;

public interface IVoteService
{
    Task CreateEntryCommentDownVote(Guid entryCommentId);
    Task CreateEntryCommentUpVote(Guid entryCommentId);
    Task CreateEntryDownVote(Guid entryId);
    Task CreateEntryUpVote(Guid entryId);
    Task DeleteEntryComment(Guid entryCommentId);
    Task DeleteEntryVote(Guid entryId);
}