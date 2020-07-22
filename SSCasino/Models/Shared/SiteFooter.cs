﻿//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// SiteFooter.cs
//      Data model that defines the site footer.
//========================================================================================================================

namespace SSCasino.Models
{
    public class SiteFooter
    {
        #region Properties
        //================================================================================================================
        //================================================================================================================

        public virtual PageErrors PageErrors { get; set; }
        public virtual UserPrompt UserPrompt { get; set; }
        public virtual UserQuestion UserQuestion { get; set; }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Properties

        #region Constructors
        //================================================================================================================
        //================================================================================================================

        public SiteFooter()
        //================================================================================================================
        // Default constructor
        //================================================================================================================
        {
            // Create data models for the generic dialogs
            PageErrors = new PageErrors();
            UserPrompt = new UserPrompt();
            UserQuestion = new UserQuestion();
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Constructors
    }
}