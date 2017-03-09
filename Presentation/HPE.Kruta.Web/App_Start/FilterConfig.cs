using System.Web.Mvc;

namespace HPE.Kruta.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new KrutaAuthorize());
            //filters.Add(new RequireHttpsAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
