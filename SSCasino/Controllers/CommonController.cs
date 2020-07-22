//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// CommonController.cs
//      This class implements a generic controller to handle controller level error handling and the common dialogs.
//========================================================================================================================

using System;
using System.Data.SqlClient;
using System.Web.Mvc;
using SSCasino.Models;
using SSCasino.Properties;

namespace SSCasino.Controllers
{
    public class CommonController : Controller
    {
        #region Error Handling
        //================================================================================================================
        //================================================================================================================

        protected override void OnException(ExceptionContext errorContext)
        //================================================================================================================
        // This action is invoked when an unhadled exception is detected.
        //
        // Returns
        //      The unhandled error page
        //
        // Developer Notes
        //      Unhandled errors are caught by this website using two methods. The first way is to make use of the 
        //      OnException method in a controller (this). The second way is to define a global Application error catcher 
        //      (see Application_Error in Global.asax.cs).
        //
        //      Both methods save the error information to a session object and eventually redirect to the UnhandledError 
        //      action in the CommonController.
        //================================================================================================================
        {
            // First indicate that the exception has been handled
            // Redirect to the error page
            errorContext.ExceptionHandled = true;
            Session[SiteHelpers.RunError] = errorContext.Exception;
            errorContext.Result = (ActionResult)RedirectToAction("UnhandledError", "Home");
        }

        public ActionResult UnhandledError()
        //================================================================================================================
        // This action is invoked when an unhadled exception is detected by this controller.
        //
        // Returns
        //      The unhandled error page as a PARTIAL VIEW.
        //
        // Developer Notes
        //      Unhandled errors are caught by this website using two methods. The first way is to make use of the 
        //      OnException method in a controller (above). The second way is to define a global Application error catcher 
        //      (see Application_Error in Global.asax.cs).
        //
        //      Both methods save the error information to a session object and eventually redirect to the UnhandledError 
        //      action in the CommonController.
        //================================================================================================================
        {
            // Develoer Notes
            //      To avoid an endless loop if an error occurrs while recording error information, a session flag is set
            //      if this happens. This session flag is checked before attempting to recording error information.
            //
            // Retrieve and record the unhandled exception
            Exception model = GetUnhandledException();
            if ((bool)Session[SiteHelpers.CriticalError] == false)
                RecordUnhandledException(model);

            return View("UnhandledError", model);
        }

        private Exception GetUnhandledException()
        //================================================================================================================
        // This routine will retrieve error information from the session object to build the exception model or it will
        // build a generic exception model if no error information is present in the session.
        //
        // Returns
        //      Exception model
        //================================================================================================================
        {
            Exception model;

            // If a session exception is found, it is used as the model and flag that a partial view is required
            if (Session[SiteHelpers.RunError] != null)
            {
                model = (Exception)Session[SiteHelpers.RunError];
            }
            else
            {
                // Build a generic exception and flag that a full view is required
                model = new Exception(Resources.Err_Unhandled)
                {
                    Source = Resources.Err_Source
                };
            }

            return model;
        }

        private void RecordUnhandledException(Exception exception)
        //================================================================================================================
        // This routine will write exception information to the database.
        //
        // Parameters
        //      exception: Run time exception
        //================================================================================================================
        {

            SSCasino_DBContext dbCasino = null;
            try
            {
                // Connect to the database
                dbCasino = new SSCasino_DBContext();

                // Write the exception data
                SqlParameter paramSource = new SqlParameter("@Source", exception.Source);
                SqlParameter paramMessage = new SqlParameter("@Message", exception.Message);
                SqlParameter paramTraget = new SqlParameter("@TargetSite", exception.TargetSite);
                SqlParameter paramTrace = new SqlParameter("@StackTrace", exception.StackTrace);
                dbCasino.Database.ExecuteSqlCommand("site_RecordException @Source, @Message, @TargetSite, @StackTrace",
                    paramSource, paramMessage, paramTraget, paramTrace);
            }
            catch
            {
                // Develoer Notes
                //      To avoid an endless loop if an error occurrs while recording error information, a session flag is set
                //      if this happens. This session flag is checked before attempting to recording error information.
                //
                //  Oops, that's a critical error!
                Session[SiteHelpers.CriticalError] = true;
            }
            finally
            {
                if (dbCasino != null)
                    dbCasino.Dispose();
            }
        }

        //================================================================================================================
        //================================================================================================================
        #endregion Error Handling

        #region Common Dialogs
        //================================================================================================================
        //================================================================================================================

        public ActionResult ShowUserPrompt()
        //================================================================================================================
        // This action is invoked when the common user message dialog is requested.
        //
        // Event Arguments
        //      prompt:         Prompt / message to be displayed to the user
        //      promptFuncName: Name of the function to execute when the dialog is closed
        //
        // Returns
        //      User message dialog partial view
        //================================================================================================================
        {
            // Retrieve the event arguments
            // Create and initialize the data model
            string prompt = !string.IsNullOrEmpty(Request.Params["arg_UserPrompt"]) ? Request.Params["arg_UserPrompt"] : "";
            string promptFuncName = !string.IsNullOrEmpty(Request.Params["arg_PromptFunction"]) ? Request.Params["arg_PromptFunction"] : "";
            UserPrompt model = new UserPrompt(prompt, promptFuncName);

            return PartialView("_UserPrompt_Dialog", model);
        }

        public ActionResult ShowUserQuestion()
        //================================================================================================================
        // This action is invoked when the common user question dialog is requested.
        //
        // Event Arguments
        //      question:    Question to be displayed to the user
        //      yesFuncName: Name of the function to execute when the YES button is pressed
        //      noFuncName:  Name of the function to execute when the NO button is pressed
        //
        // Returns
        //      User question dialog partial view
        //================================================================================================================
        {
            // Retrieve the event arguments
            // Create and initialize the data model
            string question = !string.IsNullOrEmpty(Request.Params["arg_UserQuestion"]) ? Request.Params["arg_UserQuestion"] : "";
            string yesFuncName = !string.IsNullOrEmpty(Request.Params["arg_YesFunctionName"]) ? Request.Params["arg_YesFunctionName"] : "";
            string noFuncName = !string.IsNullOrEmpty(Request.Params["arg_NoFunctionName"]) ? Request.Params["arg_NoFunctionName"] : "";
            UserQuestion model = new UserQuestion(question, yesFuncName, noFuncName);

            return PartialView("_UserQuestion_Dialog", model);
        }

        public ActionResult ShowPageErrors()
        //================================================================================================================
        // This action is invoked when the common page errors dialog is requested.
        //
        // Event Arguments
        //      errorMessages: String of error messages (Pipe Delimited)
        //
        // Returns
        //      Runtime errors dialog partial view
        //================================================================================================================
        {
            // Retrieve the event arguments
            // Create the data model
            string errorMessages = !string.IsNullOrEmpty(Request.Params["arg_PageErrors"]) ? Request.Params["arg_PageErrors"] : "";
            PageErrors model = new PageErrors();

            // Convert the pipe delimited error messages to an array of error messages
            char[] delimiter = { '|' };
            string[] messageArray = errorMessages.Split(delimiter);

            // for each error message create a new page error and add it to the error messages collection...
            foreach (string message in messageArray)
            {
                PageError pageError = new PageError(message);
                model.ErrorMessages.Add(pageError);
            }

            return PartialView("_PageErrors_Dialog", model);
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Common Dialogs
    }
}