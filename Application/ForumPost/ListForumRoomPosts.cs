using Application.Core;
using Application.DTOs.ForumRoom;
using Application.Interfaces;
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
        private readonly UserAccessor _userAccessor;

        public Handler(DataContext dataContext, UserAccessor userAccessor)
        {
            _dataContext = dataContext;
            _userAccessor = userAccessor;
        }

        public async Task<Result<List<ForumPostDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = _userAccessor.GetUserId();
            var query = _dataContext.ForumPosts
                .Where(x => x.ForumRoomId == request.ForumRoomId)
                .Where(x => x.ParentPostId == null) // main posts
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new ForumPostDto()
                {
                    Id = x.Id,
                    Description = x.Description,
                    PostContent = x.PostContent,
                    UserDisplayName = x.User != null ? x.User.DisplayName : null,
                    CreatedAt = x.CreatedAt,
                    LikeCount = x.Interactions.Count(x => x.IsLiked),
                    DislikeCount = x.Interactions.Count(x => x.IsDisliked),
                    ViewCount = x.Interactions.Count(x => !x.IsLiked && !x.IsDisliked),
                    IsLiked = userId != null ? x.Interactions.Any(x => x.UserId == userId && x.IsLiked) : false,
                    IsDisliked = userId != null ? x.Interactions.Any(x => x.UserId == userId && x.IsDisliked) : false,
                    ReplyCount = x.Replies != null ? x.Replies.Count() : 0
                }).AsNoTracking();

            var result = await query.ToListAsync(cancellationToken);

            return Result<List<ForumPostDto>>.Success(result);
        }
    }
}