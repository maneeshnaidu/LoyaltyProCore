using api.Models;

namespace api.Interfaces
{
    public interface ITokenService
    {
        (string accessToken, string refreshToken) GenerateToken(ApplicationUser user, IList<string> roles);
        string GenerateRefreshToken();
    }
}