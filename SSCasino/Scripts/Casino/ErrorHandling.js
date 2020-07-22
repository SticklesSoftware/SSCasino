//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// ErrorHandling.js
//      Javascript functions and data related to handling errors
//
// Developer Notes
//      Because callbacks are asynchronous, any code following a callback will be executed immediately, before the
//      callback has completed. Because of this, you may see a PerformCallback invoked from the EndCallback of a related
//      control. This is done to force synchronous execution of code, where required.
//========================================================================================================================

//========================================================================================================================
// Constants
//========================================================================================================================

// Internal Links
var HOME_LINK = '/Home';


//========================================================================================================================
// Variables
//========================================================================================================================

// Error reporting
var m_PageErrors = '';


//========================================================================================================================
// Routines
//========================================================================================================================

function ShowPageErrors()
//========================================================================================================================
// Display a dialog that shows the page errors returned from the server
//
// Developer Notes
//      The page error fields are created by a generic piece of code, so this routine can make assumptions about the 
//      field names.
//========================================================================================================================
{
    // Clear out any previous page errors
    m_PageErrors = "";

    // Build a pipe delimited string of page errors
    var errorCount = $('#hdnModelErrorCount').val();
    var fieldName = '';
    for (var index = 1; index <= errorCount; index++) {
        fieldName = 'hdnModelError' + index.toString();
        m_PageErrors += $('#' + fieldName).val() + "|";
    }

    // Remove the trailing pipe
    m_PageErrors = m_PageErrors.substring(0, m_PageErrors.length - 1);

    // Developer Notes
    //      Because the dialog is rendered in the footer of the home page when the page is loaded, a callback must be 
    //      performed to refresh the dialog content with the latest data.
    dlgPageErrors.Show();
    dlgPageErrors.PerformCallback();
}

function dlgPageErrors_BeginCallback(sender, eventArgs)
//========================================================================================================================
// Handle pre-callback processing for the page errors dialog
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Pass the runtime errors to the server via an event argumnet
    eventArgs.customArgs["arg_PageErrors"] = m_PageErrors;
}

function cmd_HomeButton_OnClick(sender, eventArgs)
//========================================================================================================================
// Handle click events for the HOME button on the unhandled errors page.
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Redirect to the home page
    window.location.href = HOME_LINK;
}
