using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using SlavojMVC4_1.Models;

namespace SlavojMVC4_1
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // Chcete-li umožnit uživatelům tohoto webu přihlásit se pomocí účtů z jiných webů, jako je Microsoft, Facebook či Twitter,
            // je třeba tento web aktualizovat. Další informace naleznete na webu http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
