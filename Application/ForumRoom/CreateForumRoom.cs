using Application.Core;
using MediatR;
using Persistence.Providers;
using Domain.Forum;

namespace Application.Forum;

public class CreateForumRoom
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; }
        public string? Rules { get; set; }
        public string? Description { get; set; }
        public bool IsClosed { get; set; }
        public bool IsPinned { get; set; }
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
            ForumRoom forumRoom = new()
            {
                Id = request.Id,
                Title = request.Title,
                Subtitle = request.Subtitle,
                Rules = request.Rules,
                Description = request.Description,
                IsClosed = request.IsClosed,
                IsPinned = request.IsPinned
            };

            _dataContext.ForumRooms.Add(forumRoom);

            bool succeed = await _dataContext.SaveChangesAsync() > 0;

            if (succeed)
            {
                return Result<Unit>.Success(Unit.Value);
            }
            else
            {
                return Result<Unit>.Failure("Problem creating forum room");
            }
        }
    }
}
