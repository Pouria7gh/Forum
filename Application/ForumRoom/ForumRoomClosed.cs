using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Providers;

namespace Application.Forum;

public class ForumRoomClosed
{
    public class Query : IRequest<Result<bool>>
    {
        public Guid ForumRoomId { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<bool>>
    {
        private readonly DataContext _dataContext;
        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Result<bool>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _dataContext.ForumRooms.Where(x => x.Id == request.ForumRoomId)
                .Select(x => x.IsClosed);

            var result = await query.FirstOrDefaultAsync(cancellationToken);

            return Result<bool>.Success(result);
        }
    }
}
