//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// PlayingCard.cs
//      This class represents a single playing card. 
//========================================================================================================================

using System.ComponentModel.DataAnnotations;

namespace SSCasino.Models
{
    public class PlayingCard
    {
        #region Properties
        //================================================================================================================
        //================================================================================================================
        [Key]
        public int CardPackId { get; set; }
        [Key]
        public int CardNo { get; set; }
        public int CardRank { get; set; }
        public int CardSuitId { get; set; }
        public int CardSuitOrder { get; set; }
        public string CardCode { get; set; }
        public decimal CardValue { get; set; }
        public string ImagePath { get; set; }
        public int DefaultPack { get; set; }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Properties
    }
}
