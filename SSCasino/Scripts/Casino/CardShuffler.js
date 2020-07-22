//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// CardShuffler.js
//      Javascript functions and data specific to the card shuffler page
//
// Developer Notes
//      Because callbacks are asynchronous, any code following a callback will be executed immediately, before the
//      callback has completed. Because of this, you may see a PerformCallback invoked from the EndCallback of a related
//      control. This is done to force synchronous execution of code, where required.
//========================================================================================================================


//========================================================================================================================
// FUNCTIONS
//========================================================================================================================

function cbp_CardShufflerDisplay_BeginCallback(sender, eventArgs)
//========================================================================================================================
// Handle pre-callback processing for the card shuffler card display area
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
    eventArgs.customArgs[ARG_SHUFFLE_TYPE] = GetShuffleType();
}


function cbp_CardShufflerDisplay_EndCallback(sender, eventArgs)
//========================================================================================================================
// Handle post-callback processing for the card shuffler card display area
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Clear the custom event arguments
    m_CallbackAction = ""
}

