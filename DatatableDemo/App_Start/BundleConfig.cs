using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace DatatableDemo
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {



        

            // Bootstrap style
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                      "~/Vendor/bootstrap/dist/css/bootstrap.min.css", new CssRewriteUrlTransform()));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include(
                      "~/Vendor/bootstrap/dist/js/bootstrap.min.js"));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include(
                      "~/Vendor/jquery/dist/jquery.min.js"));

            // jQuery UI
            bundles.Add(new ScriptBundle("~/bundles/jqueryui/js").Include(
                      "~/Vendor/jquery-ui/jquery-ui.min.js"));

       

            // Datatables
            bundles.Add(new ScriptBundle("~/bundles/datatables/js").Include(
                      "~/Vendor/datatables/media/js/jquery.dataTables.min.js"));

            // Datatables bootstrap
            //bundles.Add(new ScriptBundle("~/bundles/datatablesBootstrap/js").Include(
            //          "~/Vendor/datatables.net-bs/js/dataTables.bootstrap.min.js"));

            // Datatables plugins
            bundles.Add(new ScriptBundle("~/bundles/datatablesPlugins/js").Include(
                      "~/Vendor/pdfmake/build/pdfmake.min.js",
                      "~/Vendor/pdfmake/build/vfs_fonts.js",
                      "~/Vendor/datatables.net-buttons/js/buttons.html5.min.js",
                      "~/Vendor/datatables.net-buttons/js/buttons.print.min.js",
                      "~/Vendor/datatables.net-buttons/js/dataTables.buttons.min.js",
                      "~/Vendor/datatables.net-buttons-bs/js/buttons.bootstrap.min.js"));

            // Datatables style
            //bundles.Add(new StyleBundle("~/bundles/datatables/css").Include(
            //          "~/Vendor/datatables.net-bs/css/dataTables.bootstrap.min.css",
            //          "~/Vendor/Select-1.2.0/css/select.bootstrap.css"));

        


        }

    }
}