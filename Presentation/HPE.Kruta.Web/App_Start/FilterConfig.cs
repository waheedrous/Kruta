using System.Web.Mvc;

namespace HPE.Kruta.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthorizeAttribute());
            //filters.Add(new RequireHttpsAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
