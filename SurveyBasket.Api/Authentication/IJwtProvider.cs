namespace SurveyBasket.Api.Authentication;

public interface IJwtProvider
{
    (string token, int expirseIn) GenerateToken(ApplicationUser user);

    string? ValidateToken(string token);

}
