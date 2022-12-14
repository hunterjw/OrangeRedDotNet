using OrangeRedDotNet.Exceptions;
using System.Threading.Tasks;

namespace OrangeRedDotNet.Authentication
{
	/// <summary>
	/// Shared Reddit Auth behavior
	/// </summary>
	public interface IRedditAuthentication
	{
		/// <summary>
		/// Get a bearer token
		/// </summary>
		/// <returns>Bearer token</returns>
		/// <exception cref="RedditAuthenticationException"></exception>
		Task<string> GetBearerToken();

		/// <summary>
		/// Revoke the latest retrieved token
		/// </summary>
		/// <returns>Awaitable task</returns>
		Task RevokeToken();
	}
}
