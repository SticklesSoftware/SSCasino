//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// YouTubeSearchResult.cs
//      This class holds data for a single YouTube serach result
//========================================================================================================================

using System.ComponentModel.DataAnnotations;

namespace SSCasino.Models
{
    public class YouTubeSearchResult
    {
        #region Properties
        //================================================================================================================
        //================================================================================================================
        [Key]
        public string SessionKey { get; set; }
        [Key]
        public int ResultNo { get; set; }
        public string YouTubeVideoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailURL { get; set; }
        public string YouTubeURL { get; set; }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Properties
    }
}
