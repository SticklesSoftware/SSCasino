//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// YouTubeSearchRequest.cs
//      This class holds data for a YouTube API video search
//========================================================================================================================

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSCasino.Models
{
    public class YouTubeSearchRequest
    {
        #region Properties
        //================================================================================================================
        //================================================================================================================
        [Key]
        public string SessionKey { get; set; }
        public string SearchPhrase { get; set; }
        public int MaxResults { get; set; } = 10;
        public List<YouTubeSearchResult> SearchResults { get; set; }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Properties

        #region Constructors
        //================================================================================================================
        //================================================================================================================

        public YouTubeSearchRequest()
        //================================================================================================================
        // Default constructor
        //================================================================================================================
        {
            // Initialize the search results list
            SearchResults = new List<YouTubeSearchResult>();
        }

        public YouTubeSearchRequest(string sessionKey)
        //================================================================================================================
        // Alternate constructor
        //
        // Properties
        //      sessionKey: Every YouTube search uses a unique session key for local storage
        //================================================================================================================
        {
            // Default properties
            SessionKey = sessionKey;

            // Initialize the search results list
            SearchResults = new List<YouTubeSearchResult>();
        }

        public YouTubeSearchRequest(string sessionKey, string searchPhrase)
        //================================================================================================================
        // Alternate constructor
        //
        // Properties
        //      sessionKey:   Every YouTube search uses a unique session key for local storage
        //      searchPhrase: Initial search phrase
        //================================================================================================================
        {
            // Default properties
            SessionKey = sessionKey;
            SearchPhrase = searchPhrase;

            // Initialize the search results list
            SearchResults = new List<YouTubeSearchResult>();
        }

        public YouTubeSearchRequest(string sessionKey, string searchPhrase, int maxResults)
        //================================================================================================================
        // Alternate constructor
        //
        // Properties
        //      sessionKey:   Every YouTube search uses a unique session key for local storage
        //      searchPhrase: Initial search phrase
        //      maxResults:   Result list limit
        //================================================================================================================
        {
            // Default properties
            SessionKey = sessionKey;
            SearchPhrase = searchPhrase;
            MaxResults = maxResults;

            // Initialize the search results list
            SearchResults = new List<YouTubeSearchResult>();
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Constructors
    }
}
