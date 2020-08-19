//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// Shuffle.cshtml
//      Javascript functions and data to support card shguffleing
//
// Developer Notes
//      Because callbacks are asynchronous, any code following a callback will be executed immediately, before the
//      callback has completed. Because of this, you may see a PerformCallback invoked from the EndCallback of a related
//      control. This is done to force synchronous execution of code, where required.
//========================================================================================================================

//========================================================================================================================
// CONSTANTS & VARIABLES
//========================================================================================================================

// Shuffling
var m_ShuffleWarningExists = false;


//========================================================================================================================
// FUNCTIONS
//========================================================================================================================

function cbo_ShufflerCardPacks_SelectedIndexChanged(sender, eventArgs)
//========================================================================================================================
// Handle selection change events for the card pack combobox
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Refresh the card example area of the shuffler control panel
    m_CallbackAction = ACT_CHANGE_CARD_PACK;
    cbp_ShufflerCardExamples.PerformCallback();
}

function cbp_ShufflerCardExamples_BeginCallback(sender, eventArgs)
//========================================================================================================================
// Handle pre-callback processing for the card examples area
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Set the custom event arguments
    eventArgs.customArgs[ARG_ACTION] = m_CallbackAction;
    eventArgs.customArgs[ARG_CARD_PACK_ID] = cbo_ShufflerCardPacks.GetValue();
}

function cbp_ShufflerCardExamples_EndCallback(sender, eventArgs)
//========================================================================================================================
// Handle post-callback processing for the card examples area
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // If the card pack was changed, refresh the card display area
    if (m_CallbackAction == ACT_CHANGE_CARD_PACK) {
        if ($("#hdnShufflerMode").val() == 1)
            cbp_CardShufflerDisplay.PerformCallback();
        else
            cbp_RandomnessCardDisplay.PerformCallback();
    }
    else {
        // Clear the custom event arguments
        m_CallbackAction = ""
    }
}

function chk_FisherYates_ValueChanged(sender, eventArgs)
//========================================================================================================================
// Handle check changed events for the shuffle type checkboxes
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//
// Developer Notes
//      The two shuffle type checkboxes are coded to act like radio buttons. I like the look of checkboxes.
//
//      The ValueChanged event is only raised when a user interaction changes the value. This is an advantage for making
//      two checkboxes act like radio buttons because changes to the values via code will not raise this event again.
//========================================================================================================================
{
    var yatesChecked = chk_FisherYates.GetChecked();
    var naiveChecked = chk_Naive.GetChecked();

    if (yatesChecked) {
        chk_Naive.SetChecked(false);
    }
    else if ((!yatesChecked) && (!naiveChecked)) {
        chk_FisherYates.SetChecked(true);
    }
}

function chk_Naive_ValueChanged(sender, eventArgs)
//========================================================================================================================
// Handle check changed events for the shuffle type checkboxes
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//
// Developer Notes
//      The two shuffle type checkboxes are coded to act like radio buttons. I like the look of checkboxes.
//
//      The ValueChanged event is only raised when a user interaction changes the value. This is an advantage for making
//      two checkboxes act like radio buttons because changes to the values via code will not raise this event again.
//========================================================================================================================
{
    var yatesChecked = chk_FisherYates.GetChecked();
    var naiveChecked = chk_Naive.GetChecked();

    if (naiveChecked) {
        chk_FisherYates.SetChecked(false);
    }
    else if ((!naiveChecked) && (!yatesChecked)) {
        chk_Naive.SetChecked(true);
    }
}

function chk_FourCardSample_ValueChanged(sender, eventArgs)
//========================================================================================================================
// Handle check changed events for the sample size checkboxes
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//
// Developer Notes
//      The two sample size checkboxes are coded to act like radio buttons. I like the look of checkboxes.
//
//      The ValueChanged event is only raised when a user interaction changes the value. This is an advantage for making
//      two checkboxes act like radio buttons because changes to the values via code will not raise this event again.
//========================================================================================================================
{
    var fourChecked = chk_FourCardSample.GetChecked();
    var sixChecked = chk_SixCardSample.GetChecked();

    if (fourChecked) {
        // Refresh the card display area
        chk_SixCardSample.SetChecked(false);
        m_CallbackAction = ACT_CHANGE_SAMPLE_SIZE;
        cbp_RandomnessCardDisplay.PerformCallback();
    }
    else if ((!fourChecked) && (!sixChecked)) {
        // Safety check, do not allow the user to uncheck thid checkbox
        chk_FourCardSample.SetChecked(true);
    }
}

function chk_SixCardSample_ValueChanged(sender, eventArgs)
//========================================================================================================================
// Handle check changed events for the shuffle type checkboxes that are user generated
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//
// Developer Notes
//      The two shuffle type checkboxes are coded to act like radio buttons. I like the look of checkboxes.
//
//      The ValueChanged event is only raised when a user interaction changes the value. This is an advantage for making
//      two checkboxes act like radio buttons because changes to the values via code will not raise this event again.
//========================================================================================================================
{
    var fourChecked = chk_FourCardSample.GetChecked();
    var sixChecked = chk_SixCardSample.GetChecked();

    if (sixChecked) {
        // Refresh the card display area
        chk_FourCardSample.SetChecked(false);
        m_CallbackAction = ACT_CHANGE_SAMPLE_SIZE;
        cbp_RandomnessCardDisplay.PerformCallback();
    }
    else if ((!sixChecked) && (!fourChecked)) {
        // Safety check, do not allow the user to uncheck thid checkbox
        chk_SixCardSample.SetChecked(true);
    }
}

function txt_ShuffleCount_Init(sender, eventArgs)
//========================================================================================================================
// Initilaize the number of shuffles to 1
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    txt_ShuffleCount.SetText($("#hdnShuffleCount").val());
}

function txt_ShuffleCount_GotFocus(sender, eventArgs)
//========================================================================================================================
// Highligh all text in the text box
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    txt_ShuffleCount.SelectAll();
}

function txt_ShuffleCount_LostFocus(sender, eventArgs)
//========================================================================================================================
// Validate the number of shuffles input
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Get the value from the text box, assume it's good
    var numberShuffles = txt_ShuffleCount.GetText();
    var minShuffles = parseInt($("#hdnMinShuffles").val());
    var maxShuffles = parseInt($("#hdnMaxShuffles").val());
    var validInput = true;

    // No value is valid
    if (numberShuffles != "") {
        // Insure it is numeric and within range
        validInput = $.isNumeric(numberShuffles);
        if (validInput) {
            validInput = ((parseInt(numberShuffles) >= minShuffles) && (parseInt(numberShuffles) <= maxShuffles));
        }

        // Show the user message, if needed
        if (!validInput) {
            m_ShuffleWarningExists = true;
            $("#div_ShuffleWarning").slideDown();
        }
        else {
            m_ShuffleWarningExists = false;
            $("#div_ShuffleWarning").slideUp();
        }
    }
    else {
        m_ShuffleWarningExists = false;
        $("#div_ShuffleWarning").slideUp();
    }
}

function cmd_ShuffleButton_OnClick(sender, eventArgs)
//========================================================================================================================
// Handle click events for the shuffle button
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    if (m_ShuffleWarningExists) {
        $("#div_ShuffleWarning").fadeOut(100).fadeIn(100).fadeOut(100).fadeIn(100).fadeOut(100).fadeIn(100);
    }
    else {
        m_CallbackAction = ACT_SHUFFLE;

        // Based on the shuffler mode, refresh tghe proper card display area
        if (parseInt($("#hdnShufflerMode").val()) == 1)
            cbp_CardShufflerDisplay.PerformCallback();
        else
            cbp_RandomnessCardDisplay.PerformCallback();
    }
}

function cmd_ResetButton_OnClick(sender, eventArgs)
//========================================================================================================================
// Handle click events for the reset button
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Reset the number of shuffles and insure the warning is closed
    txt_ShuffleCount.SetText($("#hdnMinShuffles").val());
    $("#div_ShuffleWarning").hide();

    // Reset the shuffle options
    if (parseInt($("#hdnShufflerMode").val()) == 1) {
        // Set fisher-yates as the default
        chk_FisherYates.SetChecked(true);
        chk_Naive.SetChecked(false);
    }
    else {
        // Set the four card sample as the default
        // Refresh the results graph
        chk_FourCardSample.SetChecked(true);
        chk_SixCardSample.SetChecked(false);
        RefreshResultsGraph(ACT_RESET);
    }

    // Select the default card pack
    // Refresh the card example area of the shuffler control panel
    cbo_ShufflerCardPacks.SetSelectedIndex(0);
    m_CallbackAction = ACT_CHANGE_CARD_PACK;
    cbp_ShufflerCardExamples.PerformCallback();
}

function GetShuffleType()
//========================================================================================================================
// Get the currently selected shuffle type
//
// Parameters
//      None
//
// Returns
//      Unique id of the selected shuffle type
//========================================================================================================================
{
    // Assume Fisher-Yates shuffle
    var shuffleType = 0;

    if (chk_FisherYates.GetChecked()) {
        shuffleType = chk_FisherYates.GetValue();
    }
    else {
        shuffleType = chk_Naive.GetValue();
    }

    return (shuffleType);
}

function GetSampleSize()
//========================================================================================================================
// Get the currently selected sample size
//
// Parameters
//      None
//
// Returns
//      Unique id of the selected sample size
//========================================================================================================================
{
    // Assume Fisher-Yates shuffle
    var sampleSize = 0;

    if (chk_FourCardSample.GetChecked()) {
        sampleSize = chk_FourCardSample.GetValue();
    }
    else {
        sampleSize = chk_SixCardSample.GetValue();
    }

    return (sampleSize);
}

function cbp_ShufflerControlPanel_BeginCallback(sender, eventArgs)
//========================================================================================================================
// Handle pre-callback processing for the shuffler control panel
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Set the custom event arguments
    eventArgs.customArgs[ARG_SHUFFLE_MODE] = $("#hdnShufflerMode").val();
}

function cbp_ShufflerControlPanel_EndCallback(sender, eventArgs)
//========================================================================================================================
// Handle post-callback processing for the shuffler control panel
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // If the current action is reset, then based on the shuffler mode, 
    // reset and refresh the proper card area
    if (parseInt($("#hdnShufflerMode").val()) == 1)
        cbp_CardShufflerDisplay.PerformCallback();
    else
        cbp_RandomnessCardDisplay.PerformCallback();
}

