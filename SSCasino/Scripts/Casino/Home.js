//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// Home.js
//      Javascript functions and data specific to the home page
//
// Developer Notes
//      Because callbacks are asynchronous, any code following a callback will be executed immediately, before the
//      callback has completed. Because of this, you may see a PerformCallback invoked from the EndCallback of a related
//      control. This is done to force synchronous execution of code, where required.
//========================================================================================================================

//========================================================================================================================
// FUNCTIONS
//========================================================================================================================

function PageLoadTasks()
//========================================================================================================================
// Show the about site popup
//
// Developer Notes
//      The popup is shown only the first time the user visits the site
//========================================================================================================================
{
    // Change the website background to the gold circular gradient
    siteBody = $(document.body);
    siteBody.addClass("ssc_DefaultBodyBackground");

    // Get the site popup shown cookie
    // If the cookie does not exists, show the popup and create the cookie
    var sitePopupShown = document.cookie;
    if (sitePopupShown == "") {
        document.cookie = "SitePopupShown=1;";
        $('#div_SitePopup').fadeIn();
    }
}

function cmd_PopupCloseButton_OnClick(sender, eventArgs)
//========================================================================================================================
// Handle click events for the site popup close button
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    $('#div_SitePopup').fadeOut();
}

