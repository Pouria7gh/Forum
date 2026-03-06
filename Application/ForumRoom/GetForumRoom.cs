using Application.Core;
using Application.DTOs.ForumRoom;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Providers;

namespace Application.Forum;

public class GetForumRoom
{
    public class Query : IRequest<Result<ForumRoomDto>>
    {
        public Guid ForumRoomId { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<ForumRoomDto>>
    {
        private readonly DataContext _dataContext;

        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Result<ForumRoomDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _dataContext.ForumRooms
                .Where(x => x.Id == request.ForumRoomId)
                .Select(x => new ForumRoomDto()
                {
                    Id = x.Id,
                    Description = x.Description,
                    IsClosed = x.IsClosed,
                    IsPinned = x.IsPinned,
                    Rules = x.Rules,
                    Subtitle = x.Subtitle,
                    Title = x.Title,
                }).AsNoTracking();

            var ForumRoomDto = await query.FirstOrDefaultAsync(cancellationToken);

            return Result<ForumRoomDto>.Success(ForumRoomDto!);
        }
    }
}