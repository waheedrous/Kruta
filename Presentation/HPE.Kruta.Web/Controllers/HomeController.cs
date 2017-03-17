using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// Default action. Empty for now, just a redirection to the document queue page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //var GeneralTestFlagUniqueKey = Common.Config.KrutaConfigSectionHandler.Instance.CurrentEnvironmentConfig.Flags["GeneralTestFlagUniqueKey"].Active;
            //var DevTestFlagUniqueKey = Common.Config.KrutaConfigSectionHandler.Instance.CurrentEnvironmentConfig.Flags["DevTestFlagUniqueKey"].Active;

            //var GeneralKeyValueUniqueKey = Common.Config.KrutaConfigSectionHandler.Instance.CurrentEnvironmentConfig.KeyValues["GeneralKeyValueUniqueKey"].Value;
            //var DevKeyValueUniqueKey = Common.Config.KrutaConfigSectionHandler.Instance.CurrentEnvironmentConfig.KeyValues["DevKeyValueUniqueKey"].Value;  

            return RedirectToAction("Index", "DocumentQueue");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}