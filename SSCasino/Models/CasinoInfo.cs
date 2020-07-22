//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// CasinoInfo.cs
//      This class hold information about the casino. 
//========================================================================================================================

using SSCasino.Properties;
using System.ComponentModel.DataAnnotations;

namespace SSCasino.Models
{
    public class CasinoInfo
    {
        #region Properties
        //================================================================================================================
        //================================================================================================================
        [Key]
        public int CasinoId { get; set; }
        public string CasinoName { get; set; }
        public int DefaultCasino { get; set; }
        public string GoogleAPIKey { get; set; }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Properties
    }
}
