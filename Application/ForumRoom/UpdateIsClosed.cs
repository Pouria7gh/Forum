using Application.Core;
using MediatR;
using Persistence.Providers;

namespace Application.Forum;

public class UpdateIsClosed
{
    public class Query : IRequest<Result<Unit>>
    {
        public Guid ForumRoomId { get; set; }
    }

    public class Command : IRequestHandler<Query, Result<Unit>>
    {
        private readonly DataContext _dataContext;
        public Command(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Result<Unit>> Handle(Query request, CancellationToken cancellationToken)
        {
            var room = await _dataContext.ForumRooms.FindAsync(request.ForumRoomId);
            
            if (room == null) return Result<Unit>.Failure("Room not found");

            room.IsClosed = !room.IsClosed;

            var result = await _dataContext.SaveChangesAsync();

            return result > 0 ? Result<Unit>.Success(Unit.Value) :
                Result<Unit>.Failure("Problem updating the room");
        }
    }
}
