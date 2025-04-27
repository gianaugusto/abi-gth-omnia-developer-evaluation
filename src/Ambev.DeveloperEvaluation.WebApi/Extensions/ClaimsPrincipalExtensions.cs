using System.Security.Claims;

namespace Ambev.DeveloperEvaluation.WebApi.Extensions
{
    /// <summary>
    /// Extension methods for accessing claim values from a ClaimsPrincipal.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Retrieves the value of a specific claim from the user principal.
        /// </summary>
        /// <param name="user">The ClaimsPrincipal instance.</param>
        /// <param name="claimName">The name of the claim to retrieve.</param>
        /// <returns>The claim value if found; otherwise, null.</returns>
        public static string? GetClaimValue(this ClaimsPrincipal user, string claimName)
        {
            return user?.FindFirst(claimName)?.Value;
        }

        /// <summary>
        /// Retrieves the customer's unique identifier (CustomerId) from the user's claims.
        /// </summary>
        /// <param name="user">The ClaimsPrincipal instance.</param>
        /// <returns>The CustomerId as a Guid.</returns>
        /// <exception cref="ArgumentException">Thrown if the CustomerId claim is missing or invalid.</exception>
        public static Guid GetCustomerId(this ClaimsPrincipal user)
        {
            var customerId = user.GetClaimValue(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(customerId, out var result))
            {
                return result;
            }

            throw new ArgumentException("Customer does not have a proper authentication.");
        }

        /// <summary>
        /// Validates if the customerId from the token matches the entity customerId.
        /// </summary>
        /// <param name="user">The ClaimsPrincipal (current logged user).</param>
        /// <param name="ownerId">The customerId of the resource (e.g., Sale.CustomerId).</param>
        /// <returns>True if authorized; otherwise, false.</returns>
        public static bool IsAuthorizedForCustomer(this ClaimsPrincipal user, Guid ownerId)
        {
            var customerId = user.GetCustomerId();
            return customerId == ownerId;
        }
    }
}
