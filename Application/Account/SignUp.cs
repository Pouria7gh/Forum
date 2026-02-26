using Application.Core;
using Application.DTOs.Account;
using Application.Interfaces;
using Domain.User;
using MediatR;

namespace Application.User;

public class SignUp
{
    public class Command : IRequest<Result<SignUpDto>>
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<SignUpDto>>
    {
        private readonly Repository<AppUser> _userRepository;
        private readonly PasswordService _passwordService;

        public Handler(Repository<AppUser> userRepository, PasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }
        public async Task<Result<SignUpDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            if(await _userRepository.ExistsAsync(x => x.Email == request.Email))
            {
                return Result<SignUpDto>.Failure("An account with this email already exists.");
            }

            if (await _userRepository.ExistsAsync(x => x.Username == request.Username))
            {
                return Result<SignUpDto>.Failure("Username is already taken.");
            }

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

            return Result<SignUpDto>.Success(new() { Username = user.Username });
        }
    }
}
