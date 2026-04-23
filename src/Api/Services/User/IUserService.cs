using Api.DTOs;
using Api.Views;

namespace Api.Services;
public interface IUserService
{
    Task<UserView> Register(RegisterDTO registerDto , CancellationToken cancellationToken);
    Task<Guid> Deactivate(Guid userId);
    Task<UserView> Login(string userName, string password, CancellationToken cancellationToken);
}