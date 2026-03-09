using Application.Core;
using Application.DTOs.ForumRoom;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Providers;

namespace Application.Forum;

public class ForumPostDetails
{
    public class Query : IRequest<Result<ForumPostDto>>
    {
        public Guid PostId { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<ForumPostDto>>
    {
        private readonly DataContext _dataContext;
        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Result<ForumPostDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _dataContext.ForumPosts
                .Where(x => x.Id == request.PostId)
                .Select(x => new ForumPostDto()
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    Description = x.Description,
                    PostContent = x.PostContent,
                    UserDisplayName = x.User != null ? x.User.DisplayName : null,
                    ForumRoomId = x.ForumRoomId,
                }).AsNoTracking();

            var result = await query.FirstOrDefaultAsync(cancellationToken);

            return result != null ? Result<ForumPostDto>.Success(result) : 
                Result<ForumPostDto>.Failure("Post not found");
        }
    }
}
