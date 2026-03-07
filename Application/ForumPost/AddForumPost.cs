using Application.Core;
using Domain.Forum;
using MediatR;
using Persistence.Providers;

namespace Application.Forum;

public class AddForumPost
{
    public class Command : IRequest<Result<Unit>>
    {
        public string PostContent { get; set; } = string.Empty;
        public Guid ForumRoomId { get; set; }
        public Guid UserId { get; set; }
        public string? Description { get; set; }
        public Guid? ParentPostId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _dataContext;

        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            ForumPost forumPost = new()
            {
                PostContent = request.PostContent.Trim(),
                Description = request.Description?.Trim(),
                ForumRoomId = request.ForumRoomId,
                UserId = request.UserId,
                ParentPostId = request.ParentPostId
            };

            _dataContext.ForumPosts.Add(forumPost);

            var result = await _dataContext.SaveChangesAsync();

            return result > 0 ? Result<Unit>.Success(Unit.Value) :
                Result<Unit>.Failure("Problem adding forum post.");
        }
    }
}