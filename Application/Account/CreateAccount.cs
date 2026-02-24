using Application.Core;
using Application.Interfaces;
using Domain.User;
using MediatR;

namespace Application.User;

public class CreateAccount
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly Repository<AppUser> _userRepository;
        private readonly PasswordService _passwordService;

        public Handler(Repository<AppUser> userRepository, PasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            AppUser user = new()
            {
                DisplayName = request.DisplayName,
                Email = request.Email.ToLower(),
                EmailVerified = false,
                Username = request.Username.ToLower(),
                PasswordHashed = string.Empty,
            };

            string hashedPassword = _passwordService.HashPassword(user, request.Password);

            user.PasswordHashed = hashedPassword;

            await _userRepository.AddAsync(user);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
