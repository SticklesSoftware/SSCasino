//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// CommonDialogs.js
//      Javascript functions and data to support common dialogs
//
// Developer Notes
//      Because callbacks are asynchronous, any code following a callback will be executed immediately, before the
//      callback has completed. Because of this, you may see a PerformCallback invoked from the EndCallback of a related
//      control. This is done to force synchronous execution of code, where required.
//========================================================================================================================

//========================================================================================================================
// Variables
//========================================================================================================================

// Event arguments
var m_Action = "";

// User prompts
var m_UserPrompt = '';
var m_UserPromptFunction = '';

// User questions
var m_UserQuestion = '';
var m_UserQuestionYesFunction = '';
var m_UserQuestionNoFunction = '';

//========================================================================================================================
// User Prompt
//========================================================================================================================

function ShowUserPrompt(prompt, promptFunction)
//========================================================================================================================
// Display the given prompt / message to the user
//
// Parameters
//      prompt:         Prompt / message to display
//      promptFunction: Name of the function to invoke when the dialog is closed
//========================================================================================================================
{
    // Assign the user message and optional function
    // Display the dialog
    m_UserPrompt = prompt;
    m_UserPromptFunction = promptFunction;
    dlgUserPrompt.Show();

    // Developer Notes
    //      Because the dialog is rendered in the footer of the home page when the page is loaded, a callback must be 
    //      performed to refresh the dialog content with the latest data.
    dlgUserPrompt.PerformCallback();
}

function CloseUserPrompt()
//========================================================================================================================
// Close thew generic user prompt dialog
//========================================================================================================================
{
    dlgUserPrompt.Hide();
}

function dlgUserPrompt_BeginCallback(sender, eventArgs)
//========================================================================================================================
// Handle pre-callback processing for the generic user prompt dialog
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Set the custom event arguments
    eventArgs.customArgs["arg_UserPrompt"] = m_UserPrompt;
    eventArgs.customArgs["arg_PromptFunction"] = m_UserPromptFunction;
}


//========================================================================================================================
// User Question
//========================================================================================================================

function ShowUserQuestion(question, yesFunctionName, noFunctionName)
//========================================================================================================================
// Display the given prompt / message to the user
//
// Parameters
//      question:        Question to ask the user
//      yesFunctionName: Name of the function to invoke when the YES button is presses
//      noFunctionName:  Name of the function to invoke when the YES button is presses
//========================================================================================================================
{
    // Assign the user question and button functions
    // Display the dialog
    m_UserQuestion = question;
    m_UserQuestionYesFunction = yesFunctionName;
    m_UserQuestionNoFunction = noFunctionName;
    dlgUserQuestion.Show();

    // Developer Notes
    //      Because the dialog is rendered in the footer of the home page when the page is loaded, a callback must be 
    //      performed to refresh the dialog content with the latest data.
    dlgUserQuestion.PerformCallback();
}

function CloseUserQuestion()
//========================================================================================================================
// Close thew generic user question dialog
//========================================================================================================================
{
    dlgUserQuestion.Hide();
}

function dlgUserQuestion_BeginCallback(sender, eventArgs)
//========================================================================================================================
// Handle pre-callback processing for the generic user question dialog
//
// Parameters
//      sender:    Reference to the control that raised the event
//      eventArgs: Data associated with the event
//========================================================================================================================
{
    // Set the custom event arguments
    eventArgs.customArgs["arg_UserQuestion"] = m_UserQuestion;
    eventArgs.customArgs["arg_YesFunctionName"] = m_UserQuestionYesFunction;
    eventArgs.customArgs["arg_NoFunctionName"] = m_UserQuestionNoFunction;
}
