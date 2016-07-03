using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KindergartentManagerment
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CultureAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public const String CookieLangEntry = "language";

        public String Name { get; set; }
        public static String CookieName
        {
            get { return "_Culture"; }
        }

        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            var culture = Name;
            if (String.IsNullOrEmpty(culture))
                culture = GetSavedCultureOrDefault(filterContext.RequestContext.HttpContext.Request);

            // Set culture on current thread
            SetCultureOnThread(culture);

            // Proceed as usual
            base.OnActionExecuting(filterContext);
        }

        public static void SavePreferredCulture(HttpResponseBase response, String language,
                                                Int32 expireDays = 1)
        {
            var cookie = new HttpCookie(CookieName) { Expires = DateTime.Now.AddDays(expireDays) };
            cookie.Values[CookieLangEntry] = language;
            response.Cookies.Add(cookie);
        }

        public static String GetSavedCultureOrDefault(HttpRequestBase httpRequestBase)
        {
            var culture = "";
            var cookie = httpRequestBase.Cookies[CookieName];
            if (cookie != null)
                culture = cookie.Values[CookieLangEntry];
            return culture;
        }

        private static void SetCultureOnThread(String language)
        {
            var cultureInfo = System.Globalization.CultureInfo.CreateSpecificCulture(language);
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}