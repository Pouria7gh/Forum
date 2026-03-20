using Application.Core;
using Application.Interfaces;
using Domain.Forum;
using MediatR;
using Persistence.Providers;

namespace Application.Forum;

public class AddPostView
{
    public class Query : IRequest<Result<Unit>>
    {
        public Guid ForumPostId { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<Unit>>
    {
        private readonly DataContext _dataContext;
        private readonly UserAccessor _userAccessor;
        public Handler(DataContext dataContext, UserAccessor userAccessor)
        {
            _dataContext = dataContext;
            _userAccessor = userAccessor;
        }
        public async Task<Result<Unit>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = _userAccessor.GetUserId();
            ForumPostInteraction interaction = new()
            {
                UserId = userId, // can be null for guests
                ForumPostId = request.ForumPostId
            };

            _dataContext.ForumPostInteractions.Add(interaction);

            var result = await _dataContext.SaveChangesAsync();

            return result > 0 ? Result<Unit>.Success(Unit.Value) :
                Result<Unit>.Failure("Problem adding interaction.");
        }
    }
}