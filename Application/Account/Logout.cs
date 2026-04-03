using Application.Core;
using Application.Interfaces;
using MediatR;

namespace Application.Account;

public class Logout
{
    public class Command : IRequest<Result<Unit>> { }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly SignInService _signInService;
        public Handler(SignInService signInService)
        {
            _signInService = signInService;
        }
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _signInService.SignOutAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}