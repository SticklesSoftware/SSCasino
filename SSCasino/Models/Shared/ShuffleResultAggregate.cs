//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// ShuffleResultAggregate.cs
//      This class holds the aggregated result of a single shuffle
//========================================================================================================================

using System.ComponentModel.DataAnnotations;

namespace SSCasino.Models
{
    public class ShuffleResultAggregate
    {
        #region Properties
        //================================================================================================================
        //================================================================================================================
        [Key]
        public SiteHelpers.ShuffleTypes ShuffleType { get; set; }
        [Key]
        public string ShufflePattern { get; set; }
        public int PatternCount { get; set; }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Properties
    }
}
