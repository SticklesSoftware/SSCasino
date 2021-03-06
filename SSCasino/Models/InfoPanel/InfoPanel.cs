﻿//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// InfoPanel.cs
//      This class holds user information about the current page. 
//========================================================================================================================

using System.Collections.Generic;

namespace SSCasino.Models
{
    public class InfoPanel
    {
        #region Properties
        //================================================================================================================
        //================================================================================================================
        public SiteHelpers.InfoPanelTypes PanelType { get; set; }
        public ICollection<InfoPanelEntry> PanelEntries { get; set; }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Properties

        #region Constructors
        //================================================================================================================
        //================================================================================================================

        public InfoPanel()
        //================================================================================================================
        // Default constructor
        //================================================================================================================
        {
            // Default properties
            PanelType = SiteHelpers.InfoPanelTypes.InfoPanel;

            // Create the panel enrties collection
            PanelEntries = new HashSet<InfoPanelEntry>();
        }

        public InfoPanel(SiteHelpers.InfoPanelTypes panelType)
        //================================================================================================================
        // Alternate constructor
        //================================================================================================================
        {
            // Set properties
            PanelType = panelType;

            // Create the panel enrties collection
            PanelEntries = new HashSet<InfoPanelEntry>();
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Constructors
    }
}
