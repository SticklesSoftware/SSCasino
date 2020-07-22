//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// ShuffleResultsGraph.cs
//      This class holds data for the shuffling results graph. 
//========================================================================================================================

using System.Collections.Generic;

namespace SSCasino.Models
{
    public class ShuffleResultsGraph
    {
        #region Properties
        //================================================================================================================
        //================================================================================================================
        public int DeafultWidth { get; } = 400;
        public int DefaultHeight { get; } = 200;
        public int ResizeWidth { get; set; }
        public int ResizeHeight { get; set; }
        public int TotalShuffles { get; set; }
        public ICollection<ShuffleResultAggregate> ShuffleResultsAggregated { get; set; }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Properties

        #region Constructors
        //================================================================================================================
        //================================================================================================================

        public ShuffleResultsGraph()
        //================================================================================================================
        // Default constructor
        //================================================================================================================
        {
            // Default the width and height
            ResizeWidth = DeafultWidth;
            ResizeHeight = DefaultHeight;

            // Create the playing cards list
            ShuffleResultsAggregated = new HashSet<ShuffleResultAggregate>();
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Constructors
    }
}
