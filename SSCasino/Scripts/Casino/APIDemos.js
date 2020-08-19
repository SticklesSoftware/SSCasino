//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// APIDemos.js
//      Javascript functions and data specific to the API Demos page
//
// Developer Notes
//      Because callbacks are asynchronous, any code following a callback will be executed immediately, before the
//      callback has completed. Because of this, you may see a PerformCallback invoked from the EndCallback of a related
//      control. This is done to force synchronous execution of code, where required.
//========================================================================================================================

//========================================================================================================================
// VARIABLES
//========================================================================================================================

// YouTube search requests
var m_SearchRequestKey = "";
var m_SearchRequestPhrase = "";
var m_SelectedVideoURL = "";
var m_SelectedVideoResultNo = 0;
var m_SearchCategory = "";

// Video Playback
var m_ActiveThumbnaiRow = 0;

//========================================================================================================================
// FUNCTIONS
//========================================================================================================================

function PageLoadTasks()
//========================================================================================================================
// Resize the shuffling results chart when the page is loaded
//
// Developer Notes
//      
//========================================================================================================================
{
    // Change the website background to the green circular gradient
    siteBody = $(document.body);
    siteBody.removeClass("ssc_DefaultBodyBackground");
    siteBody.addClass("ssc_RedBodyBackground");

    // Highligh the active thumbnail row
    m_ActiveThumbnaiRow = $("#hdn_SelectedVideoResultNo").val();
    var thumbnailRows = document.getElementsByName("div_ThumbnailRow");
    thumbnailRows.item(m_ActiveThumbnaiRow - 1).classList.add("ssc_APIDemos_ThumbnailRowSelected");
}

function cbp_Thumbnails_BeginCallback(sender, eventArgs)
//========================================================================================================================
// Handle pre-callback processing for the thumbnails area
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Set the custom event arguments
    eventArgs.customArgs[ARG_SESSION_KEY] = m_SearchRequestKey;
    eventArgs.customArgs[ARG_SEARCH_PHRASE] = m_SearchRequestPhrase;
    eventArgs.customArgs[ARG_CAT_NAME] = m_SearchCategory;
}

function cbp_Thumbnails_EndCallback(sender, eventArgs)
//========================================================================================================================
// Handle post-callback processing for the thumbnails area
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Developer Notes
    //      Never clear the search category as it is used in the video player callback
    //      It will be cleared in that process
    //
    // Clear the search request data
    m_SearchRequestKey = "";
    m_SearchRequestPhrase = "";
    m_SearchCategory = "";

    m_ActiveThumbnaiRow = $("#hdn_SelectedVideoResultNo").val();
    var thumbnailRows = document.getElementsByName("div_ThumbnailRow");
    thumbnailRows.item(m_ActiveThumbnaiRow - 1).classList.add("ssc_APIDemos_ThumbnailRowSelected");

    // Always refresh the video
    m_SelectedVideoURL = $("#hdn_SelectedVideoURL").val();
    m_SelectedVideoResultNo = $("#hdn_SelectedVideoResultNo").val();
    cbp_VideoPlayer.PerformCallback();
}

function cbp_VideoPlayer_BeginCallback(sender, eventArgs)
//========================================================================================================================
// Handle pre-callback processing for the video player
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Set the custom event arguments
    eventArgs.customArgs[ARG_VIDEO_URL] = m_SelectedVideoURL;
    eventArgs.customArgs[ARG_VIDEO_RESULT_NO] = m_SelectedVideoResultNo;
}

function cbp_VideoPlayer_EndCallback(sender, eventArgs)
//========================================================================================================================
// Handle post-callback processing for the thumbnails area
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Clear the search request data
    m_SelectedVideoURL = "";
    m_SelectedVideoResultNo = 0;
}

function YouTube_MenuClick(sessionKey, searchPhrase, searchCategory)
//========================================================================================================================
// Handle clicks for the YouTube categories
//
// Parameters
//      sessionKey:    Search request key
//      searchPhrase:   Search request phrase
//      searchCategory: Category being searched
//========================================================================================================================
{
    // Save the search request data
    m_SearchRequestKey = sessionKey;
    m_SearchRequestPhrase = searchPhrase;
    m_SearchCategory = searchCategory;

    // Refresh the thumbnails area
    cbp_Thumbnails.PerformCallback();
}

function div_ThumbnailRow_OnClick(resultNo, selectedVideoURL)
//========================================================================================================================
// Handle clicks on the individual video thumbnails
//
// Parameters
//      selectedVideoURL: URL of the selected video
//========================================================================================================================
{
    if (m_ActiveThumbnaiRow != resultNo) {
        var thumbnailRows = document.getElementsByName("div_ThumbnailRow");
        thumbnailRows.item(m_ActiveThumbnaiRow - 1).classList.remove("ssc_APIDemos_ThumbnailRowSelected");

        m_ActiveThumbnaiRow = resultNo;
        thumbnailRows.item(m_ActiveThumbnaiRow - 1).classList.add("ssc_APIDemos_ThumbnailRowSelected");
        $("#hdn_SelectedVideoResultNo").val(m_ActiveThumbnaiRow);

        // Get the selected video and load it into the player
        m_SelectedVideoResultNo = resultNo;
        m_SelectedVideoURL = selectedVideoURL;
        cbp_VideoPlayer.PerformCallback();
    }
}
