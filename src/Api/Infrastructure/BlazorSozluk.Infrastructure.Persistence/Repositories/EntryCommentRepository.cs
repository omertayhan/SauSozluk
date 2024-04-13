﻿using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;
using BlazorSozluk.Infrastructure.Persistence.Repositories;

namespace BlazorSozluk.Api.Infrastructure.Persistence.Repositories;

public class EntryCommentRepository : GenericRepository<EntryComment>, IEntryCommentRepository
{
    public EntryCommentRepository(BlazorSozlukContext dbContext) : base(dbContext)
    {
    }
}
