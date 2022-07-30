namespace OrangeRedDotNet.Authentication
{
	/// <summary>
	/// Options for PasswordAuthentication
	/// </summary>
	public class PasswordAuthenticationOptions
	{
		/// <summary>
		/// Reddit app client ID
		/// </summary>
		public string ClientId { get; set; }
		/// <summary>
		/// Reddit app client secret
		/// </summary>
		public string ClientSecret { get; set; }
		/// <summary>
		/// Reddit username
		/// </summary>
		public string Username { get; set; }
		/// <summary>
		/// Reddit password
		/// </summary>
		public string Password { get; set; }
	}
}
