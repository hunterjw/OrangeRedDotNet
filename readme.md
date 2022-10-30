# OrangeRedDotNet

An incomplete .NET client for Reddit.

| Table of Contents                 |
|:----------------------------------|
| [Library](#library)               |
| [Blazor Web App](#blazor-web-app) |
| [Console App](#console-app)       |
| [Tests](#tests)                   |

## Library

### Authentication

The OrangeRedDotNet library supports application only authentication, password authentication, and OAuth authentication. All authentication methods share the `IRedditAuthentication` interface. All authentication methods support optionally specifying a load function and a save function for caching tokens retrieved from Reddit so that a new token does not need to be requested if the existing token is still valid. Tokens can be manually revoked as well via the authentication classes.

#### ApplicationOnlyAuthentication

```
using OrangeRedDotNet.Authentication;

ApplicationOnlyAuthentication appOnlyAuth = new(new ApplicationOnlyAuthenticationOptions 
{
	GrantType = APP_GRANT_TYPE,
	DoNotTrack = SET_TO_NOT_HAVE_CLIENT_TRACKED_BY_REDDIT,
	DeviceId = UNIQUE_DEVICE_ID,
	ClientId = APP_CLIENT_ID,
	ClientSecret = APP_CLIENT_SECRET
});
```

#### PasswordAuthentication

```
using OrangeRedDotNet.Authentication;

PasswordAuthentication passwordAuth = new(new PasswordAuthenticationOptions
{
	ClientId = APP_CLIENT_ID,
	ClientSecret = APP_CLIENT_SECRET,
	Username = REDDIT_USERNAME,
	Password = REDDIT_PASSWORD
});
```

#### OAuthAuthentication

```
using OrangeRedDotNet.Authentication;

OAuthAuthentication oauthAuth = new(new OAuthAuthenticationOptions
{
	ClientId = APP_CLIENT_ID,
	RedirectUrl = APP_REDIRECT_URL,
	Duration = DURATION_FOR_RETURNED_TOKEN,
	Scopes = LIST_OF_SCOPE_STRINGS,
	Compact = TO_SHOW_COMPACT_AUTHORIZATION_PAGE
});

string state = "SOME_UNIQUE_STRING";
string authorizationUrl = oauthAuth.GetAuthorizationUrl(state);

// User should navigate to the authorization URL.
// The result of the authorization is sent to the APP_REDIRECT_URL.

string code = "SUPPLIED_FROM_QUERY_PARAMETER";
string returnedState = "SUPPLIED_FROM_QUERY_PARAMETER";
string error = "SUPPLIED_FROM_QUERY_PARAMETER";
oauthAuth.ParseRedirectUrl(code, returnedState, state, error);

// OAuth Authentication object should now be ready to use
```

### The Reddit Client

The `Reddit` class is used to make requests to the Reddit API. API methods are grouped by the sections defined in the [official API docs](https://www.reddit.com/dev/api). If the Reddit client is being used within a web application hosted in a browser (i.e. Blazor Webassembly), do not provide a `RedditUserAgent` as the browsers user agent will be used instead.

```
using OrangeRedDotNet;
using OrangeRedDotNet.Authentication;

// Initialize auth object here
IRedditAuthentication auth;

// Initialize the client
Reddit client = new(auth, new RedditUserAgent
{
	Name = "APPLICATION NAME",
	Version = "APPLICATION_VERSION"
});

// Example: Get the identity of the current logged in user
var result = await client.Account.GetIdentity();
```

## Blazor Web App

To run the Blazor web app, authentication needs to be configured in `wwwroot/appsettings.json`.

```
{
	"ApplicationOnlyAuthenticationOptions": {
		"GrantType": "APP_GRANT_TYPE",
		"DoNotTrack": BOOLEAN_VALUE,
		"ClientId": "APP_CLIENT_ID"
	},
	"OAuthAuthenticationOptions": {
		"ClientId": "APP_CLIENT_ID",
		"RedirectUrl": "APP_REDIRECT_URL",
		"Duration": "AUTHENTICATION_DURATION"
	}
}
```

## Console App

To run the console app, authentication needs to be configured. Authentication can be set in either the `appsettings.json` file in the same directory as the executable or in user secrets. The console app supports `PasswordAuthentication` or `ApplicationOnlyAuthentication`. 

```
{
	"Authentication": "PasswordAuthentication or ApplicationOnlyAuthentication",
	"PasswordAuthenticationOptions": {
		"ClientId": "APP_CLIENT_ID",
		"ClientSecret": "APP_CLIENT_SECRET",
		"Username": "REDDIT_USERNAME",
		"Password": "REDDIT_PASSWORD"
	},
	"ApplicationOnlyAuthenticationOptions": {
		"GrantType": "GRANT_TYPE",
		"DoNotTrack": BOOLEAN_VALUE,
		"DeviceId": "DEVICE_ID",
		"ClientId": "APP_CLIENT_ID",
		"ClientSecret": "APP_CLIENT_SECRET"
	}
}
```

## Tests

To run the tests in the tests project, authentication will need to be configured. Optionally, a test subbreddit and test post can be configured for testing post and comment creation.

```
{
	"PasswordAuthenticationOptions": {
		"ClientId": "APP_CLIENT_ID",
		"ClientSecret": "APP_CLIENT_SECRET",
		"Username": "REDDIT_USERNAME",
		"Password": "REDDIT_PASSWORD"
	},
	"TestSubreddit": "SUBBREDDIT_NAME",
	"TestPostThingId": "POST_THING_ID"
}
```
