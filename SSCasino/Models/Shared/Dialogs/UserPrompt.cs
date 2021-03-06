﻿//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// UserPrompt.cs
//      Data model that represents a prompt or message for the user.
//========================================================================================================================

namespace SSCasino.Models
{
    public class UserPrompt
    {
        #region Properties
        //================================================================================================================
        //================================================================================================================

        public string Prompt { get; set; }
        public string PromptFunctionName { get; set; }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Properties

        #region Constructors
        //================================================================================================================
        //================================================================================================================

        public UserPrompt()
        //================================================================================================================
        // Default constructor
        //================================================================================================================
        {
        }

        public UserPrompt(string userPrompt, string promptFunctionName)
        //================================================================================================================
        // Alternate constructor
        //
        // Parameters
        //      userPrompt:         Prompt / message text for the user
        //      PromptFunctionName: Function to execute when the dialog is closed
        //================================================================================================================
        {
            // Assign properties based on parameters
            Prompt = userPrompt;
            PromptFunctionName = promptFunctionName;
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Constructors
    }
}