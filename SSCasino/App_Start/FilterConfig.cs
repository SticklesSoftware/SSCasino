//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// FilterConfig.cs
//      This class handles configuration of any application request filters.
//
// Developer Notes
//      In ASP.NET MVC, a user request is routed to the appropriate controller and action method. However, there may be 
//      circumstances where you want to execute some logic before or after an action method executes. ASP.NET MVC provides 
//      filters for this purpose.
//========================================================================================================================

using System.Web.Mvc;

namespace SSCasino
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttributeEx());
        }

        public class HandleErrorAttributeEx : HandleErrorAttribute
        {
            public HandleErrorAttributeEx() : base() { }
            public override void OnException(ExceptionContext filterContext)
            {
                base.OnException(filterContext);
                filterContext.ExceptionHandled = false;
            }
        }
    }
}
