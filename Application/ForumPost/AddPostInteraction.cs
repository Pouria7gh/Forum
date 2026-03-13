using Application.Core;
using Application.Interfaces;
using Domain.Forum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Providers;

namespace Application.Forum;

public class AddPostInteraction
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid ForumPostId { get; set; }
        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _dataContext;
        private readonly UserAccessor _userAccessor;

        public Handler(DataContext dataContext, UserAccessor userAccessor)
        {
            _dataContext = dataContext;
            _userAccessor = userAccessor;
        }
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            Guid? userId = _userAccessor.GetUserId();
            if (userId == null) return Result<Unit>.Failure("UnAuthorized.");

            var lastInteraction = await _dataContext.ForumPostInteractions
                .Where(x => x.UserId == userId && x.ForumPostId == request.ForumPostId)
                .Where(x => x.IsLiked || x.IsDisliked).FirstOrDefaultAsync();

            if (lastInteraction == null)
            {
                ForumPostInteraction interaction = new()
                {
                    UserId = (Guid)userId,
                    ForumPostId = request.ForumPostId,
                    IsLiked = request.IsLiked,
                    IsDisliked = request.IsDisliked
                };
                _dataContext.Add(interaction);
            }
            else if (lastInteraction.IsLiked == request.IsLiked && lastInteraction.IsDisliked == request.IsDisliked)
            {
                _dataContext.Remove(lastInteraction);
            }
            else if (lastInteraction.IsLiked != request.IsLiked && lastInteraction.IsDisliked != request.IsDisliked)
            {
                lastInteraction.IsLiked = request.IsLiked;
                lastInteraction.IsDisliked = request.IsDisliked;
            }
            var result = await _dataContext.SaveChangesAsync();

            return result > 0 ? Result<Unit>.Success(Unit.Value) :
                Result<Unit>.Failure("Problem adding interaction.");
        }
    }
}