using System.Web.Mvc;

namespace KindergartentManagerment.Areas.Kindergarten
{
    public class KindergartentAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Kindergarten";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Kindergarten_default",
                "Home/Kindergarten/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}