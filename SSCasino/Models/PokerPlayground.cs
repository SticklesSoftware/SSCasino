//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// PokerPlayground.cs
//      This class hold information about the poker playground. 
//========================================================================================================================

using SSCasino.Properties;

namespace SSCasino.Models
{
    public class PokerPlayground
    {
        #region Properties
        //================================================================================================================
        //================================================================================================================
        public string NoServerMessage { get; set; }
        public string SiteContact { get; set; }
        public string SiteEmail { get; set; }
        public InfoPanel SitePanel { get; set; }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Properties

        #region Constructors
        //================================================================================================================
        //================================================================================================================

        public PokerPlayground()
        //================================================================================================================
        // Default constructor
        //================================================================================================================
        {
            // Set default properties
            NoServerMessage = Properties.Resources.Site_NoServer;
            SiteContact = Properties.Resources.Site_Contact;
            SiteEmail = Properties.Resources.Site_Email;

            // Create an information panel
            SitePanel = new InfoPanel(SiteHelpers.InfoPanelTypes.SitePanel);

            // Add a new panel entry for "About This Site"
            InfoPanelEntry aboutEntry = new InfoPanelEntry(Resources.Site_Welcome, Resources.Site_About, "");
            SitePanel.PanelEntries.Add(aboutEntry);

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
            SitePanel.PanelEntries.Add(techEntry);
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Constructors
    }
}
