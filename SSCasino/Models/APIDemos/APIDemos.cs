//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// APIDemos.cs
//      This class holds data for the APIs demo page
//========================================================================================================================

using System;
using SSCasino.Properties;

namespace SSCasino.Models
{
    public class APIDemos
    {
        #region Properties
        //================================================================================================================
        //================================================================================================================
        public InfoPanel InformationPanel { get; set; }
        public YouTubeAPI YouTubeAPI { get; set; }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Properties

        #region Constructors
        //================================================================================================================
        //================================================================================================================

        public APIDemos()
        //================================================================================================================
        // Default constructor
        //================================================================================================================
        {
            // Create the YouTube API parent
            YouTubeAPI = new YouTubeAPI();

            // Create an information panel
            InformationPanel = new InfoPanel(SiteHelpers.InfoPanelTypes.InfoPanel);

            // Add a new panel entry for "About This Page"
            InfoPanelEntry aboutEntry = new InfoPanelEntry(Resources.InfoPanel_AboutHeader, Resources.About_APIDemos, "");
            InformationPanel.PanelEntries.Add(aboutEntry);

            // Add a new panel entry for "Technologies Used"
            InfoPanelEntry techEntry = new InfoPanelEntry(Resources.InfoPanel_TechHeader);

            // Add the about backend technologies line
            InfoPanelEntryLine backendLine = new InfoPanelEntryLine(Resources.InfoPanel_HeaderBackendTech, Resources.InfoPanel_AboutBackendTech);
            techEntry.EntryLines.Add(backendLine);

            // Add the about frontend technologies line
            InfoPanelEntryLine frontendLine = new InfoPanelEntryLine(Resources.InfoPanel_HeaderFrontendTech, Resources.InfoPanel_AboutFrontendTech);
            techEntry.EntryLines.Add(frontendLine);

            // Add the about the development environment technologies line
            InfoPanelEntryLine devLine = new InfoPanelEntryLine(Resources.InfoPanel_HeaderDevEnv, Resources.InfoPanel_AboutDevEnv);
            techEntry.EntryLines.Add(devLine);

            // Add the panel entry
            InformationPanel.PanelEntries.Add(techEntry);
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Constructors
    }
}
