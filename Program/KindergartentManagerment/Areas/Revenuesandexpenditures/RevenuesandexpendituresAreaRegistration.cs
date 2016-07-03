using System.Web.Mvc;

namespace KindergartentManagerment.Areas.Revenuesandexpenditures
{
    public class RevenuesandexpendituresAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Revenuesandexpenditures";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Revenuesandexpenditures_default",
                "Home/Revenuesandexpenditures/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}