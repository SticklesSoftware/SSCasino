//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// Casino.js
//      Javascript functions and data common to the site
//
// Developer Notes
//      Because callbacks are asynchronous, any code following a callback will be executed immediately, before the
//      callback has completed. Because of this, you may see a PerformCallback invoked from the EndCallback of a related
//      control. This is done to force synchronous execution of code, where required.
//========================================================================================================================

//========================================================================================================================
// CONSTANTS
//========================================================================================================================

// Callback actions
var ACT_SHUFFLE = "act_Shuffle";
var ACT_CHANGE_CARD_PACK = "act_ChangeCardPack";
var ACT_RESET = "act_Reset";
var ACT_CHANGE_SAMPLE_SIZE = "act_ChangeSampleSize";
var ACT_RESIZE_CHART = "act_ResizeChart";

// Callback Arguments
var ARG_ACTION = "arg_Action";
var ARG_CARD_PACK_ID = "arg_CardPackId";
var ARG_SHUFFLE_MODE = "arg_ShuffleMode";
var ARG_SHUFFLE_TYPE = "arg_ShuffleType";
var ARG_SHUFFLE_COUNT = "arg_ShuffleCount";
var ARG_SAMPLE_SIZE = "arg_SampleSize";
var ARG_CARD_AREA_WIDTH = "arg_CardAreaWidth";
var ARG_CARD_AREA_HEIGHT = "arg_CardAreaHeight";
var ARG_CARD_SAMPLES_HEIGHT = "arg_CardSamplesHeight";
var ARG_SESSION_KEY = "arg_SessionKey";
var ARG_SEARCH_PHRASE = "arg_SearchPhrase";
var ARG_VIDEO_URL = "arg_VideoURL";
var ARG_VIDEO_RESULT_NO = "arg_VideoResultNo";
var ARG_CAT_NAME = "arg_CategoryName";


//========================================================================================================================
// VARIABLES
//========================================================================================================================

// Callbacks
var m_CallbackAction = "";


//========================================================================================================================
// FUNCTIONS
//========================================================================================================================

function PageLoad()
//========================================================================================================================
// Handle resize eventrs for the site
//========================================================================================================================
{
    // Determine if the page being loaded has the PageLoadTasks function implemented. If so, it is called
    // to perform page specific tasks during page initialization.
    if (window.PageLoadTasks != null)
        window.PageLoadTasks();
}

function PageUnload()
//========================================================================================================================
// Handle resize eventrs for the site
//========================================================================================================================
{
    // Determine if the page being loaded has the PageUnloadTasks function implemented. If so, it is called
    // to perform page specific tasks during page unload.
    if (window.PageUnloadTasks != null)
        window.PageUnloadTasks();
}

function PageResize()
//========================================================================================================================
// Handle resize eventrs for the site
//========================================================================================================================
{
    // Determine if the page being loaded has the PageResizeTasks function implemented. If so, it is called
    // to perform page specific tasks during page resize.
    if (window.PageResizeTasks != null)
        window.PageResizeTasks();
}

function img_InfoPanelHandle_OnClick(sender, eventArgs)
//========================================================================================================================
// Display or hide the informationm panel
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Toggle the panel
    $("#div_InfoPanleContent").slideToggle();
}

function div_MiniMenu_OnClick(sender, eventArgs)
//========================================================================================================================
// Display the mini menu
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//
// Developer Notes
//      The site's mini menu is implemented using a DevExpress callback panel.  It has functionality built into it to
//      detect when the user clicks out and automatically closes it.  A callback is performed to get the menu items and
//      the menu is displayed in the EndCallback event.
//
//      In order to display the mini menu at the mouse coordianates, they are saved here and used again in the EndCallback
//      event of the mini menu.
//========================================================================================================================
{
    // Display the popup menu at the mouse click
    // Save the mouse coordinates
    m_PageMouseX = event.pageX;
    m_PageMouseY = event.pageY;

    // Display the min menu
    pop_MiniMenu.PerformCallback();
}

function pop_MiniMenu_EndCallback(sender, eventArgs)
//========================================================================================================================
// Handle post-callback processing for the card shuffler card display area
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Display the mini-menu at the mouse position
    pop_MiniMenu.ShowAtPos(m_PageMouseX, m_PageMouseY);
}

