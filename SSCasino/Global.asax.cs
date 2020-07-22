//========================================================================================================================
// Description
//      This class handles all application and session level events
//========================================================================================================================
using SSCasino.Models;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using EnhancedViewLocations;
using System.Web;
using DevExpress.Security.Resources;

namespace SSCasino {
    public class MvcApplication : System.Web.HttpApplication 
    {
        #region Application Events
        //========================================================================================================================
        //========================================================================================================================

        protected void Application_Start()
        //========================================================================================================================
        // Perform processing in response to an application start event.
        //
        // Developer Notes
        //      The application start event is fired the first time a user visits the site and before the session is created.
        //========================================================================================================================
        {
            // Deveoper Notes
            //      Areas: Define a set separate of Controllers, Models and Views specifically for the pages in that area
            //
            // Register any custom areas
            AreaRegistration.RegisterAllAreas();

            // Configure the Web.api based on settings in WebApiConfig
            // Register any request filters
            // Register any custom request routes
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Deveoper Notes
            //      Out of the box, VS2019 does not allow the addition of subfolders under the Views folder. This can
            //      cause organizational issues for a website with many views. To fix this drawback, a third party
            //      product called EnhancedViewLocations is used that provides the desired functionality.
            //
            // Register additional view folders
            RegisterCustomViewFolders();

            // DevExpress initialization

            // Developer Notes
            //      When DevExpress editors are bound to corresponding data model fields by using their Bind methods, the 
            //      DevExpressEditorsBinder model binder must be used to correctly transfer values from DevExpress editors back 
            //      to the corresponding data model fields.
            // Assign the DevExpress model binder as the default
            ModelBinders.Binders.DefaultBinder = new DevExpress.Web.Mvc.DevExpressEditorsBinder();

            // Insure server-based grid filtering is case-insensitive
            // Allow images to be loaded from any directory or URL
            DevExpress.Data.Helpers.ServerModeCore.DefaultForceCaseInsensitiveForAnySource = true;
            DevExpress.Security.Resources.AccessSettings.StaticResources.SetRules(DirectoryAccessRule.Allow(), UrlAccessRule.Allow());

            // Global error handing for application errors
            DevExpress.Web.ASPxWebControl.CallbackError += Application_Error;
        }

        private void RegisterCustomViewFolders()
        //================================================================================================================
        // Register custom view folders
        //
        // Developer Notes
        //      When a view name is referenced in code, MVC will search for the view in both the "Views" and the 
        //      "Views/Shared" folders. If you create other subfolders under the "Views" folder MVC does not know about 
        //      them. This does not leave much room for proper organization as the "Shared" folder should really only 
        //      contain views that are shared across the entire site. To solve this problem I use a product called 
        //      Enhanced View Locator which is used to register other view folders with MVC.
        //================================================================================================================
        {
            // Give priority to the standard view locations so we don't always search our
            // custom folders first when searching for a view.
            EnhancedViewLocator.EnableStandardRazorCSLocations();

            // Add custom view folders
            EnhancedViewLocator.AddFolder("Views/APIDemos");
            EnhancedViewLocator.AddFolder("Views/CardShuffler");
            EnhancedViewLocator.AddFolder("Views/PokerRoom");
            EnhancedViewLocator.AddFolder("Views/Randomness");
            EnhancedViewLocator.AddFolder("Views/Shared/Dialogs");
            EnhancedViewLocator.AddFolder("Views/Shared/Dialogs/PageErrors");
            EnhancedViewLocator.AddFolder("Views/Shared/Dialogs/UserPrompt");
            EnhancedViewLocator.AddFolder("Views/Shared/Dialogs/UserQuestion");
            EnhancedViewLocator.AddFolder("Views/Shared/Forms");
            EnhancedViewLocator.AddFolder("Views/Shared/MiniMenu");
            EnhancedViewLocator.AddFolder("Views/Shared/Shuffler");

            // Install a custom view engine to manage the lookups.
            EnhancedViewLocator.Install(ControllerBuilder.Current);
        }

        protected void Application_Error(object sender, EventArgs e)
        //================================================================================================================
        // Handle any unhandles exceptions thrown from within the website
        //
        // Developer Notes
        //      Unhandled errors are caught by this website using two methods. The first way is to make use of the 
        //      OnException method in a controller (see the file CommonController.cs). The second way is to define a 
        //      global Application error catcher (see Application_Error in Global.asax.cs).
        //
        //      Both methods save the error information to a session object and eventually redirect to the UnhandledError 
        //      action in the CommonController.
        //================================================================================================================
        {
            // Get the latest exception and save it to a session variable
            Exception exception = HttpContext.Current.Server.GetLastError();
            Session[SiteHelpers.RunError] = exception;

            // Developer Notes
            //      Because this event is raised as a last resort for catching an unhandled error, it has no knowledge of
            //      controllers or action methods so, unfortunately, the redirect needs to be a hard coded path.
            Response.Redirect("/Home/UnhandledError");
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Application Events

        #region Session Events
        //========================================================================================================================
        //========================================================================================================================

        protected void Session_start(object source, EventArgs eventArgs)
        //========================================================================================================================
        // Perform processing in response to a session start event. The session start event is fired the first time a user visits
        // the site or continues browsing after the session has timed out.
        //
        // Parameters
        //      source:    Reference to the object that raised the event
        //      eventArgs: Data associated with the event
        //
        // Developer Notes
        //      The site does not require any authentication.
        //========================================================================================================================
        {
            // Create and initialize the critical error flag session variable
            Session[SiteHelpers.CriticalError] = false;

            // Attempt to connect to the server
            Session[SiteHelpers.ServerRunning] = SiteHelpers.CheckServerConnection();
        }

        //========================================================================================================================
        //========================================================================================================================
        #endregion  // Session Events
    }
}