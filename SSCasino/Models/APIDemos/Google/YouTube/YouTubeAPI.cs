//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// YouTubeAPI.cs
//      Parent class for interfacing with the YouTube API
//========================================================================================================================

using System.Collections.Generic;

namespace SSCasino.Models
{
    public class YouTubeAPI
    {
        #region Properties
        //================================================================================================================
        //================================================================================================================
        public string YouTubeMenuTitle { get; set; }
        public string YouTubeCategoryTitle { get; set; }
        public List<YouTubeCategory> YouTubeMenu { get; set; }
        public YouTubeSearchRequest SearchRequest { get; set; }
        public string SelectedCategory { get; set; }
        public string SelectedVideoURL { get; set; }
        public int SelectedVideoResultNo { get; set; }
        public bool DataLimitExceeded { get; set; } = false;
        public bool UseDatabaseFirst { get; set; } = false;

        //================================================================================================================
        //================================================================================================================
        #endregion  // Properties

        #region Constructors
        //================================================================================================================
        //================================================================================================================

        public YouTubeAPI()
        //================================================================================================================
        // Default constructor
        //================================================================================================================
        {
            // Initialize and populate the YouTube categories menu
            YouTubeMenuTitle = Properties.Resources.PokerMenuTitle;
            YouTubeCategoryTitle = Properties.Resources.PokerCategoryTitle;
            YouTubeMenu = new List<YouTubeCategory>
            {
                new YouTubeCategory(Properties.Resources.PokerBeats_Key, Properties.Resources.PokerBeats_Phrase, Properties.Resources.PokerBeats_Caption),
                new YouTubeCategory(Properties.Resources.PokerBluffs_Key, Properties.Resources.PokerBluffs_Phrase, Properties.Resources.PokerBluffs_Caption),
                new YouTubeCategory(Properties.Resources.PokerHands_Key, Properties.Resources.PokerHands_Phrase, Properties.Resources.PokerHands_Caption),
                new YouTubeCategory(Properties.Resources.PokerFights_Key, Properties.Resources.PokerFights_Phrase, Properties.Resources.PokerFights_Caption)
            };
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Constructors
    }
}
