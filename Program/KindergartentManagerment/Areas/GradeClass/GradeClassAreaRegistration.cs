using System.Web.Mvc;

namespace KindergartentManagerment.Areas.GradeClass
{
    public class GradeClassAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GradeClass";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GradeClass_default",
                "Home/GradeClass/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}