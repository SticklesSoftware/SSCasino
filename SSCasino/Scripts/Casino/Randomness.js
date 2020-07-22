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

var m_CardAreaWidth = 0;
var m_CardAreaHeight = 0;
var m_CardSamplesHeight = 0;

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
    // Get the new width of the parent container and refresh the results graph
    m_CardAreaWidth = document.getElementById('div_CardArea').offsetWidth;
    m_CardAreaHeight = document.getElementById('div_CardArea').offsetHeight;
    m_CardSamplesHeight = document.getElementById('div_CardSamples').offsetHeight;

    // Refresh the shuffling results graph
    m_CallbackAction = ACT_RESIZE_CHART;
    graph_ShuffleResults.PerformCallback();
}

function PageResizeTasks()
//========================================================================================================================
// Resize the shuffling results chart when the browser window resizes
//
// Developer Notes
//      
//========================================================================================================================
{
    if (graph_ShuffleResults.InCallback() == false) {
        // Get the new width of the parent container and refresh the results graph
        m_CardAreaWidth = document.getElementById('div_CardArea').offsetWidth;
        m_CardAreaHeight = document.getElementById('div_CardArea').offsetHeight;
        m_CardSamplesHeight = document.getElementById('div_CardSamples').offsetHeight;

        // Refresh the shuffling results graph
        m_CallbackAction = ACT_RESIZE_CHART;
        graph_ShuffleResults.PerformCallback();
    }
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
    // If the current action is change sample size or reset, the results graph needs to be updated aslo
    if ((m_CallbackAction == 'act_ChangeSampleSize') || (m_CallbackAction == 'act_Reset'))
        graph_ShuffleResults.PerformCallback();
    else
        m_CallbackAction = ""
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
    eventArgs.customArgs[ARG_CARD_AREA_WIDTH] = m_CardAreaWidth;
    eventArgs.customArgs[ARG_CARD_AREA_HEIGHT] = m_CardAreaHeight;
    eventArgs.customArgs[ARG_CARD_SAMPLES_HEIGHT] = m_CardSamplesHeight;
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
    // Clear the event argument variables
    m_CallbackAction = "";
}
