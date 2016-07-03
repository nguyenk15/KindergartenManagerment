using System.Web.Mvc;

namespace KindergartentManagerment.Areas.Nutritious
{
    public class NutritiousAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Nutritious";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Nutritious_default",
                "Home/Nutritious/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}