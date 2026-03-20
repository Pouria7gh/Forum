using Application.Core;
using Application.DTOs.ForumRoom;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Providers;

namespace Application.Forum;

public class ListPostReplies
{
    public class Query : IRequest<Result<PagedList<ForumPostDto>>>
    {
        public Guid ParentPostId { get; set; }
        public PagingParams PagingParams { get; set; } = new();
    }

    public class Handler : IRequestHandler<Query, Result<PagedList<ForumPostDto>>>
    {
        private readonly DataContext _dataContext;
        private readonly UserAccessor _userAccessor;
        public Handler(DataContext dataContext, UserAccessor userAccessor)
        {
            _dataContext = dataContext;
            _userAccessor = userAccessor;
        }
        public async Task<Result<PagedList<ForumPostDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = _userAccessor.GetUserId();
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
                    UserDisplayName = x.User != null ? x.User.DisplayName : null,
                    LikeCount = x.Interactions.Count(x => x.IsLiked),
                    DislikeCount = x.Interactions.Count(x => x.IsDisliked),
                    IsLiked = userId != null ? x.Interactions.Any(x => x.UserId == userId && x.IsLiked) : false,
                    IsDisliked = userId != null ? x.Interactions.Any(x => x.UserId == userId && x.IsDisliked) : false,
                });

            var result = await PagedList<ForumPostDto>.CreateAsync(query,
                request.PagingParams.CurrentPage, request.PagingParams.PageSize);

            return Result<PagedList<ForumPostDto>>.Success(result);
        }
    }
}