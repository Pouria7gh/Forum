namespace Application.Interfaces;

public interface SignInService
{
    Task SignInAsync(Guid userId);

    Task SignOutAsync();
}