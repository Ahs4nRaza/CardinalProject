using CardinalProject.Models;
using System.Collections.Generic;
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
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                new Claim("HospitalId", user.HospitalId?.ToString() ?? string.Empty),
                new Claim("Department", user.Department ?? string.Empty)
            };
        }

        // Extension methods to extract specific claims easily
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            return idClaim != null && int.TryParse(idClaim.Value, out var id) ? id : 0;
        }

        public static int GetRoleId(this ClaimsPrincipal user)
        {
            var roleClaim = user.FindFirst(ClaimTypes.Role);
            return roleClaim != null && int.TryParse(roleClaim.Value, out var roleId) ? roleId : 0;
        }

        public static int? GetHospitalId(this ClaimsPrincipal user)
        {
            var hospitalClaim = user.FindFirst("HospitalId");
            if (hospitalClaim != null && int.TryParse(hospitalClaim.Value, out var hospitalId))
                return hospitalId;
            return null;
        }

        public static string GetDepartment(this ClaimsPrincipal user)
        {
            return user.FindFirst("Department")?.Value ?? string.Empty;
        }
    }
}
