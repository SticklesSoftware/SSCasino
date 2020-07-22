//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// InfoPanel.cs
//      This class holds user information about the current page. 
//========================================================================================================================

using System.Collections.Generic;

namespace SSCasino.Models
{
    public class InfoPanelEntry
    {
        #region Properties
        //================================================================================================================
        //================================================================================================================
        public string EntryTitle { get; set; }
        public ICollection<InfoPanelEntryLine> EntryLines { get; set; }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Properties

        #region Constructors
        //================================================================================================================
        //================================================================================================================

        public InfoPanelEntry()
        //================================================================================================================
        // Default constructor
        //================================================================================================================
        {
            // Initialize the collection of entry lines
            EntryLines = new HashSet<InfoPanelEntryLine>();
        }

        public InfoPanelEntry(string entryTitle)
        //================================================================================================================
        // Alternate constructor
        //
        // Parameters
        //      entryTitle: Title for the entry
        //================================================================================================================
        {
            // Initialize the collection of entry lines
            EntryLines = new HashSet<InfoPanelEntryLine>();

            // Create just a header
            EntryTitle = entryTitle;
        }

        public InfoPanelEntry(string entryTitle, string entryLineHeader, string entryLineBody)
        //================================================================================================================
        // Alternate constructor
        //
        // Parameters
        //      entryTitle:      Title for the entry
        //      entryLineHeader: Header text for the entry line
        //      entryLineBody:   Body text for the entry line
        //================================================================================================================
        {
            // Initialize the collection of entry lines
            EntryLines = new HashSet<InfoPanelEntryLine>();

            // Create a single line entry from the parameters
            EntryTitle = entryTitle;
            InfoPanelEntryLine entryLine = new InfoPanelEntryLine(entryLineHeader, entryLineBody);
            EntryLines.Add(entryLine);
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Constructors
    }
}
