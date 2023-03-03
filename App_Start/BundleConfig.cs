﻿using System.Web;
using System.Web.Optimization;

namespace MeterReaderCMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-1.10.2.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/admin/scripts").Include(
                                                "~/Scripts/nagishli.js",
                                                "~/Scripts/bootstrap.min.js",
                                                "~/Scripts/main.js"
                                            ));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate.js", "~/Scripts/jquery.validate.unobtrusive.js"));
            // JQuery validator.            
            bundles.Add(new ScriptBundle("~/bundles/track-custom-validator").Include("~/Scripts/script-track-custom-validator.js"));
        }
    }
}
