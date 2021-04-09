using System.Web;
using System.Web.Mvc;

namespace DSW2_Proyecto_Huron_Azul
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
