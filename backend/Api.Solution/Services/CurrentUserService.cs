using Api.Solution.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Solution.Services
{
    /// <summary>
    /// This Service is Responsible to get data from the token bearer.
    /// Call this only inside controllers, to keep other services from depending on IHttpContext
    /// </summary>
    public class CurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            return Guid.Parse(userId);
        }

        public string GetEmail()
        {
            var userEmail = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

            if (userEmail == null)
                throw new ArgumentNullException(nameof(userEmail));

            return userEmail;
        }
    }
}
