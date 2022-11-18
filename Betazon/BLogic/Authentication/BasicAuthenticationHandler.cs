using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Text;
using Betazon.BLogic.ConnectionDb;

namespace Betazon.BLogic.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        UserManager user = new UserManager("Data Source=.\\sqlExpress;Initial Catalog=ProvaAdventure;Integrated Security=True;TrustServerCertificate=true;");
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
            ) : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Response.Headers.Add("WWW-Authenticate", "Basic");

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Autorizzazione mancante"));
            }

            var authorizationHeader = Request.Headers["Authorization"].ToString();

            var authoHeaderRegEx = new Regex("Basic (.*)");

            if (!authoHeaderRegEx.IsMatch(authorizationHeader))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authorization Code, not properly formatted"));
            }

            var authBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(authoHeaderRegEx.Replace(authorizationHeader, "$1")));
            var authSplit = authBase64.Split(Convert.ToChar(":"), 2);

            var authUser = authSplit[0];
            var authPassword = authSplit.Length > 1 ? authSplit[1] : throw new Exception("Unable to get Password");

            if (!user.getAdmin(authUser, authPassword))
            {
                return Task.FromResult(AuthenticateResult.Fail("User e/o password errati !!!"));
            }

            var authenticatedUser = new AuthenticatedUser("BasicAuthentication", true, authUser);

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authenticatedUser));

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }
    }
}
