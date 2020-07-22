//========================================================================================================================
// WEBSITE: Poker Playground - Experimental virtual poker
//
// SiteHelpers.cs
//      This class contains data and reusable methods common to the website.
//========================================================================================================================

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using SSCasino.Models;

namespace SSCasino
{
    public static class SiteHelpers
    {
        #region Enumerations
        //================================================================================================================
        //================================================================================================================

        // Info Panel Types
        public enum InfoPanelTypes
        {
            InfoPanel = 1,
            SitePanel = 2
        }

        // Card Suits
        public enum CardSuits
        {
            Hearts = 1,
            Diamonds = 2,
            Clubs = 3,
            Spades = 4
        }

        // Shuffle Mode
        public enum ShuffleMode
        {
            Standard = 1,
            Comparison = 2
        }

        // Shuffle Types
        public enum ShuffleTypes
        {
            FisherYates = 1,
            Naive = 2
        }

        // Sample Sizes
        public enum SampleSizes
        {
            FourCardSample = 1,
            SixCardSample = 2,
            FullDeck = 3
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Enumerations

        #region Constants
        //================================================================================================================
        //================================================================================================================

        // Session Variables
        public static string CriticalError = "CriticalError";
        public static string ServerRunning = "ServerRunning";
        public static string RunError = "RunError";
        public static string ResultsGraphWidth = "ResultsGraphWidth";
        public static string ResultsGraphHeight = "ResultsGraphHeight";

        // Error Handling
        public static string ModelErrKey = "ModelError";
        public static string ErrorEvenRowClass = "ssc_CallStackEntryEven";
        public static string ErrorOddRowClass = "ssc_CallStackEntryOdd";

        // System Colors
        public static string DialogBkColor = "e5e5e5";
        public static string MenuBackolor = "e5e5e5";
        public static string GridDeadAreaColor = "999999";

        // Shuffler
        public static string ShufflerWarning = "Valid range: {0} to {1}";
        public static string FourCardSampleCards = "JS QH KD AC";
        public static string SixCardSampleCards = "AS 2H 3D 4C 5S 6H";
        public static ICollection<ShuffleResult> CombinedShuffleResults;

        // Cards
        public static int SuitCount = 13;

        // Info Panel
        public static string TechInfoLine = "{0} {1}";

        //================================================================================================================
        //================================================================================================================
        #endregion  // Constants

        #region General
        //================================================================================================================
        //================================================================================================================

        public static bool CheckServerConnection()
        //================================================================================================================
        // Check to see if the server is running
        //
        // Returns
        //      True/False
        //================================================================================================================
        {
            bool serverIsRunning = true;

            // Attempt to connect to the server
            SSCasino_DBContext dbCasino = null;
            try
            {
                // Attempt to open a database connection
                dbCasino = new SSCasino_DBContext();
                dbCasino.Database.CommandTimeout = 2;
                dbCasino.Database.Connection.Open();
                if ((dbCasino.Database.Connection.State == System.Data.ConnectionState.Closed) || (dbCasino.Database.Connection.State == System.Data.ConnectionState.Broken))
                    serverIsRunning = false;
            }
            catch
            {
                serverIsRunning = false;
            }
            finally
            {
                if (dbCasino != null)
                    dbCasino.Dispose();
            }

            return serverIsRunning;
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Constants

        #region Shuffling
        //================================================================================================================
        //================================================================================================================

        public static ShuffledPackage ShuffleCards(CardPack cardPack, ShuffleTypes shuffleType, int shuffleCount, bool recordResults)
        //================================================================================================================
        // Shuffle the given pack of cards the specified nyumber of times using the given algorithm
        //
        // Parameters
        //      cardPack:      Reference to a pack of cards
        //      shuffleType:   Shuffling alroithm to use
        //      shuffleCount:  Number of times to shuffle
        //      recordResults: Flag, record the results of each shuffle
        //      
        // Returns
        //      a shuufled package
        //================================================================================================================
        {
            ShuffledPackage shuffledPackage;

            if (shuffleType == ShuffleTypes.FisherYates)
                shuffledPackage = ShuffleCards_FisherYates(cardPack, shuffleCount, recordResults);
            else
                shuffledPackage = ShuffleCards_Naive(cardPack, shuffleCount, recordResults);

            return shuffledPackage;
        }

        public static void ClearCombinedShuffleResults()
        //================================================================================================================
        // Clear the combined shuffle results collection
        //================================================================================================================
        {
            if (CombinedShuffleResults != null)
                CombinedShuffleResults.Clear();
        }

        private static ShuffledPackage ShuffleCards_FisherYates(CardPack cardPack, int shuffleCount, bool recordResults)
        //================================================================================================================
        // Shuffle the given pack of cards using the Fisher-Yates algorithm
        //
        // Parameters
        //      cardPack:      Reference to a pack of cards
        //      shuffleCount:  Number of times to shuffle
        //      recordResults: Flag, record the results of each shuffle
        //      
        // Returns
        //      A shuffled package
        //================================================================================================================
        {
            // Create a shuffled package and a random number generator
            ShuffledPackage shuffledPackage = new ShuffledPackage();

            // Shuffle the card pack the specified number of times
            int randomIndex;
            int cardsInPack;
            for (int i = 1; i <= shuffleCount; i++)
            {
                // get the number of cards in the deck
                // Create a random number generator
                cardsInPack = cardPack.CardDeck.Count;
                RNGCryptoServiceProvider randomProvider = new RNGCryptoServiceProvider();

                // Shuffle the deck
                int packBottom = cardsInPack;
                while (packBottom > 1)
                {
                    // Get a random number between 0 and the bottom of the pack
                    randomIndex = GetRandomNumber(randomProvider, packBottom);

                    // Decrement the bottom of the pack (zero based collections)
                    packBottom--;

                    //Swap the playing card at the random index with the playing card at the bottom of the pack
                    PlayingCard selectedCard = cardPack.CardDeck[randomIndex];
                    cardPack.CardDeck[randomIndex] = cardPack.CardDeck[packBottom];
                    cardPack.CardDeck[packBottom] = selectedCard;
                }

                // Record the shuffle result
                if (recordResults)
                    RecordShuffleResult(cardPack, SiteHelpers.ShuffleTypes.FisherYates, i, shuffledPackage.ShuffleResults);
            }

            // After all the shuffling is complete assign the final card pack
            shuffledPackage.CardPack = cardPack;

            return shuffledPackage;
        }

        private static ShuffledPackage ShuffleCards_Naive(CardPack cardPack, int shuffleCount, bool recordResults)
        //================================================================================================================
        // Shuffle the given pack of cards using the naive algorithm
        //
        // Parameters
        //      cardPack:      Reference to a pack of cards
        //      shuffleCount:  Number of times to shuffle
        //      recordResults: Flag, record the results of each shuffle
        //      
        // Returns
        //      A shuffled package
        //================================================================================================================
        {
            // Create a shuffled package
            ShuffledPackage shuffledPackage = new ShuffledPackage();

            // Shuffle the card pack the specified number of times
            int randomIndex;
            int cardsInPack;
            for (int i = 1; i <= shuffleCount; i++)
            {
                // get the number of cards in the deck
                // Create a random number generator
                cardsInPack = cardPack.CardDeck.Count;
                Random randomNumberGen = new Random();

                // Shuffle the deck
                for (int cardIndex = 0; cardIndex <= (cardsInPack - 1); cardIndex++)
                {
                    // Get a random number between 0 and the number of cards in the deck
                    randomIndex = randomNumberGen.Next(0, (cardsInPack - 1));

                    // Swap the card at the current index with the card at the random index
                    PlayingCard selectedCard = cardPack.CardDeck[randomIndex];
                    cardPack.CardDeck[randomIndex] = cardPack.CardDeck[cardIndex];
                    cardPack.CardDeck[cardIndex] = selectedCard;
                }

                // Record the shuffle result
                if (recordResults)
                    RecordShuffleResult(cardPack, SiteHelpers.ShuffleTypes.Naive, i, shuffledPackage.ShuffleResults);
            }

            // After all the shuffling is complete assign the final card pack
            shuffledPackage.CardPack = cardPack;

            return shuffledPackage;
        }

        private static int GetRandomNumber(RNGCryptoServiceProvider randomProvider, int maxValue)
        //================================================================================================================
        // Get a random number between 0 and maxValue
        //
        // Parameters
        //      randomProvider: Random number generator
        //      maxValue:       Maximum value to return
        //
        // Developer Notes
        //      The random number generator built into C# does not produce unique results overe time. I used it initially
        //      and the Fisher-Yates shuffle results were not at all what was expected. Many more duplicates than expected
        //      and missing patterns.
        //================================================================================================================
        {
            byte[] box = new byte[1];
            do 
                randomProvider.GetBytes(box);
            while (!(box[0] < maxValue * (Byte.MaxValue / maxValue)));

            return (box[0] % maxValue);
        }
        
        private static void RecordShuffleResult(CardPack cardPack, ShuffleTypes shuffleType, int shuffleNo, ICollection<ShuffleResult> shuffleResults)
        //================================================================================================================
        // Add the given shuffle results to the collection
        //
        // Parameters
        //      cardPack:       Shuffled pack of cards
        //      shuffleResults: Collection of shuffle results
        //================================================================================================================
        {
            // Create a new shuffle result
            ShuffleResult shuffleResult = new ShuffleResult
            {
                ShuffleType = shuffleType,
                ShuffleNo = shuffleNo
            };

            // Loop through each card in the deck
            foreach (PlayingCard card in cardPack.CardDeck)
            {
                // The shuffle pattern is determined by building a string fom the card codes
                shuffleResult.ShufflePattern += card.CardCode.Substring(0, 1);
            }

            // Add the shuffle result to the collection
            shuffleResults.Add(shuffleResult);
        }

        //================================================================================================================
        //================================================================================================================
        #endregion  // Shuffling
    }
}