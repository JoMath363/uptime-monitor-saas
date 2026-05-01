using System.Security.Claims;

namespace Api.Solution.Utils
{
    public class Helper
    {
        public static Guid GetCurrentUserId(ClaimsPrincipal user)
        {
            var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) 
                throw new ArgumentNullException(nameof(userId));

            return Guid.Parse(userId);
        }

        public static string GetCurrentUserEmail(ClaimsPrincipal user)
        {
            var userEmail = user?.FindFirst(ClaimTypes.Email)?.Value;

            if (userEmail == null)
                throw new ArgumentNullException(nameof(userEmail));

            return userEmail;
        }
    }
}
