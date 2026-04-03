using Application.Core;
using Application.DTOs.Account;
using Application.Interfaces;
using Domain.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Providers;

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
        private readonly DataContext _dataContext;
        private readonly PasswordService _passwordService;
        private readonly SignInService _signInService;

        public Handler(DataContext dataContext, PasswordService passwordService, SignInService signInService)
        {
            _dataContext = dataContext;
            _passwordService = passwordService;
            _signInService = signInService;
        }
        public async Task<Result<SignUpDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            if(await _dataContext.Users.AnyAsync(x => x.Email == request.Email.ToLower()))
            {
                return Result<SignUpDto>.Failure("An account with this email already exists.");
            }

            if (await _dataContext.Users.AnyAsync(x => x.Username == request.Username.ToLower()))
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

            _dataContext.Users.Add(user);

            var result = await _dataContext.SaveChangesAsync();

            if (result > 0)
            {
                await _signInService.SignInAsync(user.Id);
                return Result<SignUpDto>.Success(new() { UserId = user.Id });
            }
            
            return Result<SignUpDto>.Failure("Problem Signing up");
        }
    }
}
