//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// Randomness.js
//      Javascript functions and data specific to the randomness page
//
// Developer Notes
//      Because callbacks are asynchronous, any code following a callback will be executed immediately, before the
//      callback has completed. Because of this, you may see a PerformCallback invoked from the EndCallback of a related
//      control. This is done to force synchronous execution of code, where required.
//========================================================================================================================

//========================================================================================================================
// VARIABLES
//========================================================================================================================

// Results graph - force autosize
var m_ResultsGraphWidth = 0;
var m_ResultsGraphHeight = 0;
var m_GraphInRefresh = false;

//========================================================================================================================
// FUNCTIONS
//========================================================================================================================

function PageLoadTasks()
//========================================================================================================================
// Perform page load tasks
//========================================================================================================================
{
    // Change the website background to the green circular gradient
    siteBody = $(document.body);
    siteBody.addClass("ssc_GreenBodyBackground");

    // Refresh the results graph
    RefreshResultsGraph(ACT_RESIZE_CHART);
}

function PageResizeTasks()
//========================================================================================================================
// Resize the shuffling results chart when the browser window resizes
//
// Developer Notes
//      
//========================================================================================================================
{
    // Refresh the results graph
    if (m_GraphInRefresh == false) {
        RefreshResultsGraph(ACT_RESIZE_CHART);
    }
}

function RefreshResultsGraph(action)
//========================================================================================================================
// Resize the shuffling results chart when the browser window resizes
//
// Paraneters
//      action: Action to be performed
//========================================================================================================================
{
    // Get the new width of the parent container and refresh the results graph
    m_ResultsGraphWidth = document.getElementById('div_RandomnessContainer').offsetWidth;
    m_ResultsGraphHeight = document.getElementById('div_ResultsGraph').offsetHeight;

    // Refresh the shuffling results graph
    m_GraphInRefresh = true;
    m_CallbackAction = action;
    graph_ShuffleResults.PerformCallback();
}

function cbp_RandomnessCardDisplay_BeginCallback(sender, eventArgs)
//========================================================================================================================
// Handle pre-callback processing for the randomness carparison card display area
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Set the custom event arguments
    eventArgs.customArgs[ARG_ACTION] = m_CallbackAction;
    eventArgs.customArgs[ARG_CARD_PACK_ID] = cbo_ShufflerCardPacks.GetValue();
    eventArgs.customArgs[ARG_SHUFFLE_COUNT] = txt_ShuffleCount.GetText();
    eventArgs.customArgs[ARG_SAMPLE_SIZE] = GetSampleSize();
}

function cbp_RandomnessCardDisplay_EndCallback(sender, eventArgs)
//========================================================================================================================
// Handle post-callback processing for the randomness carparison card display area
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // If the sample size changed, refresh the results graph
    if ((m_CallbackAction == ACT_CHANGE_SAMPLE_SIZE) || (m_CallbackAction == ACT_SHUFFLE)) {
        RefreshResultsGraph(m_CallbackAction);
    }
    else {
        // Clear the callback action
        m_CallbackAction = ""
    }
}

function graph_ShuffleResults_BeginCallback(sender, eventArgs)
//========================================================================================================================
// Handle pre-callback processing for the shuffling results chart
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Set the custom event arguments
    eventArgs.customArgs[ARG_ACTION] = m_CallbackAction;
    eventArgs.customArgs[ARG_RESULTS_GRAPH_WIDTH] = m_ResultsGraphWidth;
    eventArgs.customArgs[ARG_RESULTS_GRAPH_HEIGHT] = m_ResultsGraphHeight;
}

function graph_ShuffleResults_EndCallback(sender, eventArgs)
//========================================================================================================================
// Handle post-callback processing for the shuffling results chart
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    m_GraphInRefresh = false;

    // Clear the event argument variables
    m_CallbackAction = "";
}
