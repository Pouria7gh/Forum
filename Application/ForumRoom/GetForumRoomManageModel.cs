using Application.Core;
using Application.DTOs.ForumRoom;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Providers;

namespace Application.Forum;

public class GetForumRoomManageModel
{
    public class Query : IRequest<Result<ForumRoomManageDto>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<ForumRoomManageDto>>
    {
        private readonly DataContext _dataContext;
        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Result<ForumRoomManageDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _dataContext.ForumRooms
                .Where(x => x.Id == request.Id)
                .Select(x => new ForumRoomManageDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    IsClosed = x.IsClosed
                });

            var result = await query.FirstOrDefaultAsync(cancellationToken);

            return result != null ? Result<ForumRoomManageDto>.Success(result) :
                Result<ForumRoomManageDto>.Success(new());
        }
    }
}
