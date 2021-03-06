﻿using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;
using SlavojMVC4_1.ModelBinders;
using System.Globalization;
using System.Threading;

namespace SlavojMVC4_1
{
    // Poznámka: Pokyny k povolení klasického režimu služeb IIS6 a IIS7 
    // naleznete na webu: http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //přemístěno z InitializeSimpleMembershipAttribute.cs
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            AreaRegistration.RegisterAllAreas();

            GlobalFilters.Filters.Add(new SlavojMVC4_1.Models.MainMenuSessionRepository());


            WebApiConfig.Register(System.Web.Http.GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            System.Web.Mvc.ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());




        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "d.M.yyyy";
            newCulture.DateTimeFormat.DateSeparator = ".";
            Thread.CurrentThread.CurrentCulture = newCulture;
        }
    }
}