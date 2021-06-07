using System.Web;
using System.Web.Optimization;

namespace ArDesignForminhas_Web
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/fontawesome").Include(
                        "~/Scripts/fontawesome.js"));

            bundles.Add(new ScriptBundle("~/bundles/DataTable").Include(
                        "~/Scripts/DataTable.js"));

            bundles.Add(new ScriptBundle("~/bundles/sweetalert").Include(
                "~/Scripts/sweetalert.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryMask").Include(
                        "~/Scripts/jquery.mask.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/fileinput").Include(
                        "~/Scripts/fileinput.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/fasTheme").Include(
                        "~/Content/bootstrap-fileinput/themes/fas/theme.js"));

            bundles.Add(new ScriptBundle("~/bundles/fasThemefileinput").Include(
                        "~/Content/bootstrap-fileinput/themes/explorer-fas/theme.js"));

            bundles.Add(new ScriptBundle("~/bundles/fileInputPt-BR").Include(
                "~/Scripts/locales/pt-BR.js"));

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            // pronto para a produção, utilize a ferramenta de build em https://modernizr.com para escolher somente os testes que precisa.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/DataTable.css",
                      "~/Content/sweetalert.css",
                      "~/Content/bootstrap-fileinput/css/fileinput.min.css",
                      "~/Content/bootstrap-fileinput/themes/explorer-fa/theme.min.css"));
        }
    }
}
