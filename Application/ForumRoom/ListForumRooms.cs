using Application.Core;
using Application.DTOs.ForumRoom;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Providers;

namespace Application.Forum;

public class ListForumRooms
{
    public class Query : IRequest<Result<List<ForumRoomDto>>>
    {

    }

    public class Handler : IRequestHandler<Query, Result<List<ForumRoomDto>>>
    {
        private readonly DataContext _dataContext;

        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Result<List<ForumRoomDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _dataContext.ForumRooms
                .OrderByDescending(x => x.IsPinned)
                .ThenByDescending(x => x.CreatedAt)
                .Select(x => new ForumRoomDto()
                {
                    Id = x.Id,
                    Description = x.Description,
                    IsClosed = x.IsClosed,
                    IsPinned = x.IsPinned,
                    Rules = x.Rules,
                    Subtitle = x.Subtitle,
                    Title = x.Title
                });

            var forumRooms = await query.ToListAsync(cancellationToken);

            return Result<List<ForumRoomDto>>.Success(forumRooms);
        }
    }
}
