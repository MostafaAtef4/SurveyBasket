using Microsoft.AspNetCore.Identity;

namespace SurveyBasket.Api.Entities;

public sealed class ApplicationUser :IdentityUser
{
    public string FristName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public List<RefreshToken> RefreshTokens { get; set; } = [];

}
