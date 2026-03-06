using Application.Core;
using Application.DTOs.ForumRoom;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Providers;

namespace Application.Forum;

public class ListForumRoomPosts
{
    public class Query : IRequest<Result<List<ForumPostDto>>>
    {
        public Guid ForumRoomId { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<List<ForumPostDto>>>
    {
        private readonly DataContext _dataContext;

        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result<List<ForumPostDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _dataContext.ForumPosts
                .Where(x => x.ForumRoomId == request.ForumRoomId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new ForumPostDto()
                {
                    Description = x.Description,
                    PostContent = x.PostContent,
                    UserDisplayName = x.User!.DisplayName
                });

            var result = await query.ToListAsync(cancellationToken);

            return Result<List<ForumPostDto>>.Success(result);
        }
    }
}