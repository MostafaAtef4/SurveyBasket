using OneOf;

namespace SurveyBasket.Api.Services;

public interface IAuthService
{
   // Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
    Task<Result> RevokRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
}
