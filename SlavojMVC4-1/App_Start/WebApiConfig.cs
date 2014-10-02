using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SlavojMVC4_1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Zrušením označení následujícího řádku kódu jako komentáře povolíte v dotazu podporu akcí s návratovým typem IQueryable nebo IQueryable<T>.
            // Chcete-li se vyhnout zpracování neočekávaných nebo škodlivých dotazů, pomocí nastavení ověření u atributu QueryableAttribute proveďte ověření příchozích dotazů.
            // Další informací naleznete na webu: http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }
    }
}