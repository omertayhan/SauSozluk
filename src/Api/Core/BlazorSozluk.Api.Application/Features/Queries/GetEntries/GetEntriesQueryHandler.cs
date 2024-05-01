using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Api.Application.Features.Queries.GetEntries;
public class GetEntriesQueryHandler : IRequestHandler<GetEntriesQuery, List<GetEntriesViewModel>>
{
    private readonly IEntryRepository _entryRepository; // dependency injection
    private readonly IMapper _mapper;

    public GetEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
    {
        _entryRepository = entryRepository;
        _mapper = mapper;
    }

    public async Task<List<GetEntriesViewModel>> Handle(GetEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        if (request.TodayEntries)
        {
            query = query
                .Where(i => i.CreatedDate >= DateTime.Now.Date)
                .Where(i => i.CreatedDate <= DateTime.Now.AddDays(1).Date);
        }

        query.Include(i => i.EntryComments)
            .OrderBy(i => Guid.NewGuid())
            .Take(request.Count);

        return await query.ProjectTo<GetEntriesViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
