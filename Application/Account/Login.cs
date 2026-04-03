using Application.Core;
using Application.DTOs.Account;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Providers;

namespace Application.Account;

public class Login
{
    public class Command : IRequest<Result<Unit>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _dataContext;
        private readonly PasswordService _passwordService;
        private readonly SignInService _signInService;
        public Handler(DataContext dataContext, PasswordService passwordService, SignInService signInService)
        {
            _dataContext = dataContext;
            _passwordService = passwordService;
            _signInService = signInService;
        }
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email, 
                cancellationToken: cancellationToken);

            if (user == null)
            {
                return Result<Unit>.Failure("Email or Password is wrong.");
            }

            var result = _passwordService.VerifyPassword(user, request.Password, user.PasswordHashed);
            
            if (result)
            {
                await _signInService.SignInAsync(user.Id);
                return Result<Unit>.Success(Unit.Value);
            }

            return Result<Unit>.Failure("Email or Password is wrong.");
        }
    }
}