using System.Web;
using System.Web.Optimization;

namespace TUI.Sandbox
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/Home/jquery").Include(
            "~/Scripts/jquery-1.10.2.js",
            "~/Scripts/Customized/HomeScript.js"));

            bundles.Add(new StyleBundle("~/FlightDetails/css").Include(
                  "~/Content/bootstrap.css",
                  "~/Content/Customised/FlightsStyle.css",
                  "~/Content/Customised/SharedStyle.css"));

            bundles.Add(new StyleBundle("~/Index/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Customised/HomeStyle.css",
                      "~/Content/Customised/RespHomeStyle.css",
                      "~/Content/Customised/SharedStyle.css"));

            bundles.Add(new StyleBundle("~/Shared/css").Include(
                "~/Content/bootstrap.css",
              "~/Content/Customised/HomeStyle.css",
              "~/Content/Customised/SharedStyle.css"));

        }
    }
}
