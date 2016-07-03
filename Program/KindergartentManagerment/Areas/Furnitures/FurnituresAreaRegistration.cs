using System.Web.Mvc;

namespace KindergartentManagerment.Areas.Furnitures
{
    public class FurnituresAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Furnitures";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Furnitures_default",
                "Furnitures/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}