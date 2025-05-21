using CardinalProject.Models;
using System.Security.Claims;

namespace CardinalProject.Helpers
{
    public static class ClaimsHelper
    {
        public static List<Claim> GetUserClaims(User user)
        {
            return new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.RoleId.ToString())
        };
        }
    }
}
