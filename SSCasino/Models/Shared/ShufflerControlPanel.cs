//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// ShufflerControlPanel.cs
//      This class implements the shuffler control panel. 
//========================================================================================================================

using System.Collections.Generic;

namespace SSCasino.Models
{
    public class ShufflerControlPanel
    {
        private const int MIN_SHUFFLES_STANDARD = 1;
        private const int MAX_SHUFFLES_STANDARD = 10000;
        private const int MIN_SHUFFLES_COMPARE = 300000;
        private const int MAX_SHUFFLES_COMPARE = 1000000;

        #region Properties
        //================================================================================================================
        //================================================================================================================
        public ICollection<CardPack> CardPacks { get; set; }
        public CardPack SelectedCardPack { get; set; }
        public int ShuffleCount { get; set; }
        public int MinShuffles { get; set; }
        public int MaxShuffles { get; set; }
        public string ShufflerWarning { get; set; }
        public SiteHelpers.ShuffleTypes ShuffleType { get; set; }
        public SiteHelpers.SampleSizes SampleSize { get; set; }
        public SiteHelpers.ShuffleMode ShuffleMode { get; set; }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Properties

        #region Constructors
        //================================================================================================================
        //================================================================================================================

        public ShufflerControlPanel()
        //================================================================================================================
        // Default constructor
        //================================================================================================================
        {
            // Set defaults
            MinShuffles = MIN_SHUFFLES_STANDARD;
            MaxShuffles = MAX_SHUFFLES_STANDARD;
            ShuffleCount = MIN_SHUFFLES_STANDARD;
            ShufflerWarning = string.Format(SiteHelpers.ShufflerWarning, MinShuffles, MaxShuffles);
            ShuffleMode = SiteHelpers.ShuffleMode.Standard;
            ShuffleType = SiteHelpers.ShuffleTypes.FisherYates;
            SampleSize = SiteHelpers.SampleSizes.FourCardSample;

            // Create the playing cards list
            CardPacks = new HashSet<CardPack>();
        }

        public ShufflerControlPanel(SiteHelpers.ShuffleMode shuffleMode)
        //================================================================================================================
        // Alternate constructor
        //================================================================================================================
        {
            // Set properties based on the mode
            ShuffleMode = shuffleMode;
            if (shuffleMode == SiteHelpers.ShuffleMode.Comparison)
            {
                MinShuffles = MIN_SHUFFLES_COMPARE;
                MaxShuffles = MAX_SHUFFLES_COMPARE;
                ShuffleCount = MIN_SHUFFLES_COMPARE;
            }
            else
            {
                MinShuffles = MIN_SHUFFLES_STANDARD;
                MaxShuffles = MAX_SHUFFLES_STANDARD;
                ShuffleCount = MIN_SHUFFLES_STANDARD;
            }

            ShufflerWarning = string.Format(SiteHelpers.ShufflerWarning, MinShuffles, MaxShuffles);
            ShuffleType = SiteHelpers.ShuffleTypes.FisherYates;
            SampleSize = SiteHelpers.SampleSizes.FourCardSample;

            // Create the playing cards list
            CardPacks = new HashSet<CardPack>();
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Constructors
    }
}
