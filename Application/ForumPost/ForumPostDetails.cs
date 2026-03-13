using Application.Core;
using Application.DTOs.ForumRoom;
using Application.Interfaces;
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
        private readonly UserAccessor _userAccessor;
        public Handler(DataContext dataContext, UserAccessor userAccessor)
        {
            _dataContext = dataContext;
            _userAccessor = userAccessor;
        }
        public async Task<Result<ForumPostDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = _userAccessor.GetUserId();
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
                    LikeCount = x.Interactions.Count(x => x.IsLiked),
                    DislikeCount = x.Interactions.Count(x => x.IsDisliked),
                    ViewCount = x.Interactions.Count(x => !x.IsLiked && !x.IsDisliked),
                    IsLiked = userId != null ? x.Interactions.Any(x => x.UserId == userId && x.IsLiked) : false,
                    IsDisliked = userId != null ? x.Interactions.Any(x => x.UserId == userId && x.IsDisliked) : false,
                }).AsNoTracking();

            var result = await query.FirstOrDefaultAsync(cancellationToken);

            return result != null ? Result<ForumPostDto>.Success(result) : 
                Result<ForumPostDto>.Failure("Post not found");
        }
    }
}
