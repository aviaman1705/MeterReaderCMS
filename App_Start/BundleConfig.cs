﻿using System.Web;
using System.Web.Optimization;

namespace MeterReaderCMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/bs-jq-bundle").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/jquery-1.10.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate.js", "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin/js").Include(
                     "~/Scripts/fontawesome.js",
                     "~/Scripts/popper.min.js",
                     "~/Scripts/bootstrap.min.js",
                     "~/Scripts/perfect-scrollbar.min.js",
                     "~/Scripts/smooth-scrollbar.min.js",
                     "~/Scripts/chartjs.min.js"));

            // JQuery validator.
            bundles.Add(new ScriptBundle("~/bundles/custom-validator").Include("~/Scripts/script-track-custom-validator.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
        }
    }
}
