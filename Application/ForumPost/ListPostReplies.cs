using Application.Core;
using Application.DTOs.ForumRoom;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Providers;

namespace Application.Forum;

public class ListPostReplies
{
    public class Query : IRequest<Result<List<ForumPostDto>>>
    {
        public Guid ParentPostId { get; set; }
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
                .Where(x => x.ParentPostId == request.ParentPostId)
                .OrderBy(x => x.CreatedAt)
                .Select(x => new ForumPostDto()
                {
                    CreatedAt = x.CreatedAt,
                    Description = x.Description,
                    ForumRoomId = x.ForumRoomId,
                    Id = x.Id,
                    PostContent = x.PostContent,
                    UserDisplayName = x.User != null ? x.User.DisplayName : null
                });

            var result = await query.ToListAsync(cancellationToken);

            return result != null ? Result<List<ForumPostDto>>.Success(result) :
                Result<List<ForumPostDto>>.Success([]);
        }
    }
}