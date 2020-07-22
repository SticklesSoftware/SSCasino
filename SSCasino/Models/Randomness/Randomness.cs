//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// CardShuffler.cs
//      This class holds the randomness comparison page data 
//========================================================================================================================

using System;
using SSCasino.Properties;

namespace SSCasino.Models
{
    public class Randomness
    {
        #region Properties
        //================================================================================================================
        //================================================================================================================
        public InfoPanel InformationPanel;
        public ShuffledPackage ShuffledPackageFisher { get; set; }
        public ShuffledPackage ShuffledPackageNaive { get; set; }
        public ShuffleResultsGraph ShuffleResultsData { get; set; }
        public ShufflerControlPanel ControlPanel { get; set; }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Properties

        #region Constructors
        //================================================================================================================
        //================================================================================================================

        public Randomness()
        //================================================================================================================
        // Default constructor
        //================================================================================================================
        {
            // Initialize members
            ShuffledPackageFisher = new ShuffledPackage();
            ShuffledPackageNaive = new ShuffledPackage();
            ShuffleResultsData = new ShuffleResultsGraph();

            // Create and initailize the shuffler control panel
            ControlPanel = new ShufflerControlPanel(SiteHelpers.ShuffleMode.Comparison);

            // Create an information panel
            InformationPanel = new InfoPanel(SiteHelpers.InfoPanelTypes.InfoPanel);

            // Add a new panel entry for "About This Page"
            string fisherLink = "<a class='ssc_InfoPanelLink' href='https://medium.com/@oldwestaction/randomness-is-hard-e085decbcbb2' target='_blank'>Fisher-Yates</a>";
            string naiveLink = "<a class='ssc_InfoPanelLink' href='https://medium.com/@oldwestaction/randomness-is-hard-e085decbcbb2' target='_blank'>Naive</a>";
            string aboutInfo = String.Format(Resources.About_Randomness, fisherLink, naiveLink);
            InfoPanelEntry aboutEntry = new InfoPanelEntry(Resources.InfoPanel_AboutHeader, aboutInfo, "");
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
