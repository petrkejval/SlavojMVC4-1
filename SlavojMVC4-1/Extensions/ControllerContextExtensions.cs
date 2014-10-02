using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlavojMVC4_1.Extensions
{
    using System.Web.Mvc;

    public static class ControllerContextExtensions
    {
        public static bool IsRazorView(this ControllerContext executingContext)
        {
#if MVC3
            var razorViewToken = executingContext.RouteData.DataTokens[Areas.Razor.RazorAreaRegistration.RazorViewToken];
            return razorViewToken != null && razorViewToken.Equals(true);
#else
            return false;
#endif
        }
    }
}