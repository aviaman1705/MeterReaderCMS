﻿using MeterReaderCMS.Infrastructure;
using MeterReaderCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MeterReaderCMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            AutomappWebProfile.Run();
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new RequireHttpsAttribute());
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");
        }

        protected void Application_AuthenticateRequest()
        {
            // Check if user logged in
            if (User == null) { return; }

            // Get username
            string username = Context.User.Identity.Name;

            // declare array of roles
            string[] roles = null;

            using (MeterReaderDB db = new MeterReaderDB())
            {
                // populate roles
                User dto = db.Users.FirstOrDefault(x => x.Username == username);
                roles = dto.Roles.Select(x => x.RoleName).ToArray();
            }

            // Build IPrincipal object
            IIdentity userIdentity = new GenericIdentity(username);
            IPrincipal newUserObj = new GenericPrincipal(userIdentity, roles);

            // Update Context.User

            Context.User = newUserObj;
        }
    }
}
