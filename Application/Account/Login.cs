using Application.Core;
using Application.DTOs.Account;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Providers;

namespace Application.Account;

public class Login
{
    public class Command : IRequest<Result<LoginDto>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<Command, Result<LoginDto>>
    {
        private readonly DataContext _dataContext;
        private readonly PasswordService _passwordService;

        public Handler(DataContext dataContext, PasswordService passwordService)
        {
            _dataContext = dataContext;
            _passwordService = passwordService;
        }
        public async Task<Result<LoginDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email, 
                cancellationToken: cancellationToken);

            if (user == null)
            {
                return Result<LoginDto>.Failure("Email or Password is wrong.");
            }

            var result = _passwordService.VerifyPassword(user, request.Password, user.PasswordHashed);
            
            if (result)
            {
                var roles = await _dataContext.Roles
                    .Where(x => x.UserRoles.Any(x => x.UserId == user.Id))
                    .Select(x => x.Name)
                    .ToListAsync(cancellationToken);
                
                return Result<LoginDto>.Success(new() { UserId = user.Id, Roles = roles});
            }
            else
            {
                return Result<LoginDto>.Failure("Email or Password is wrong.");
            }
        }
    }
}